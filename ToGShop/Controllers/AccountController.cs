using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.ViewModels.AuthViewModels;
using Core;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting.Internal;

namespace ToGShop.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager, IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
        }
        

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {

            if (!ModelState.IsValid) return View(registerViewModel);

            ApplicationUser newUser = new ApplicationUser
            {
                FullName = registerViewModel.FullName,
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email
            };

            var identityResult = await _userManager.CreateAsync(newUser, registerViewModel.Password);
            

            if (identityResult.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var confirmationLink = Url.Action("ConfirmEmail", "Account", new { token, email = registerViewModel.Email }, Request.Scheme);


                //var msg = $"<a style=\"text-decoration:none;\" href=\"{confirmationLink}\">Təsdiqlə</a>";

                var msgArea =
                    $"<div style=\"border:2px solid #a2a2a2; border-radius:8px; text-align:center;\"><h3 style=\"background:#a2a2a2; color:white; border: 2px solid #a2a2a2; border-radius:8px;text-align:center;\">ToG Shopping - Email Təsdiqləmə</h3><p>Sizi ToG Shopping-də gördüyümüz üçün şad olduq ! Aşağıdakı keçid linkinə tıklayın və emailinizi təsdiqləyərək hesabınızı aktivləşdirin! Diqqətiniz üçün təşəkkürlər :) </p><br><a style=\"text-decoration:none;\" href=\"{confirmationLink}\">Təsdiqlə</a></div>";





                bool emailResponse = SendEmail(registerViewModel.Email, msgArea);

                if (emailResponse)
                    return RedirectToAction("ConfirmedEmail", "Account");
            }
            else
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(registerViewModel);
            }


            
            return RedirectToAction("Login", "Account");

        }

        public ActionResult ConfirmedEmail()
        {
            return View();
        }

        public ActionResult ConfirmationEmail()
        {
            return View();
        }

        public bool SendEmail(string userEmail, string msgArea)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("contact.togshop@gmail.com");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Email Təsdiq Mesajı - ToG Shopping";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = msgArea;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("contact.togshop@gmail.com", "ohtiqxjdkojlpqez");
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                // log exception
            }
            return false;
        }

        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound();

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return View(result.Succeeded ? "ConfirmationEmail" : "Error");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {

            if (!ModelState.IsValid) return View(loginViewModel);
            ApplicationUser user = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if (user==null)
            {
                ModelState.AddModelError(string.Empty, "Email və ya şifrə yanlışdır !");
                return View(loginViewModel);
            }

            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError(string.Empty, "Zəhmət olmasa emailinizi təsdiqləyin! Təsdiq mesajı emailə göndərilmişdir !");
                return View(loginViewModel);
            }

            var signInResult =
                await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe,
                    true);
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty,"Zəhmət olmasa bir neçə dəqiqə gözləyin !");
                return View(loginViewModel);
            }


            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Email və ya şifrə yanlışdır !");
                return View(loginViewModel);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }




        public IActionResult FacebookLogin(string returnUrl)
        {
            string redirectUrl = Url.Action("SocialMediaResponse", "Account", new {returnUrl = returnUrl});
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", redirectUrl);
            return new ChallengeResult("Facebook", properties);
        }

        public IActionResult GoogleLogin(string returnUrl)
        {
            string redirectUrl = Url.Action("SocialMediaResponse", "Account", new { returnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        public async Task<IActionResult> SocialMediaResponse(string returnUrl)
        {
            var loginInfo = await _signInManager.GetExternalLoginInfoAsync();
            if (loginInfo==null)
            {
                return RedirectToAction("Register");
            }
            else
            {
                var result =
                    await _signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider, loginInfo.ProviderKey, true);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    if (loginInfo.Principal.HasClaim(scl=>scl.Type==ClaimTypes.Email))
                    {
                        ApplicationUser user = new ApplicationUser()
                        {
                            Email = loginInfo.Principal.FindFirstValue(ClaimTypes.Email),
                            FullName = loginInfo.Principal.FindFirstValue(ClaimTypes.Name),
                            UserName = loginInfo.Principal.FindFirstValue(ClaimTypes.Surname),
                            EmailConfirmed = true
                        };
                        var createResult = await _userManager.CreateAsync(user);
                        if (createResult.Succeeded)
                        {
                            var identityLogin = await _userManager.AddLoginAsync(user, loginInfo);
                            if (identityLogin.Succeeded)
                            {
                                await _signInManager.SignInAsync(user, true);
                                return Redirect("Login");
                            }
                        }
                    }
                }
            }

            return RedirectToAction("Register");
        }




        public IActionResult ResetPass(string token, string email)
        {
            if (token == null && email == null)
            {
                ModelState.AddModelError("", "Axtardığınız email ilə bağlanmış hesab yoxdur ! ");
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPass(ResetPassViewModel reset)
        {
            if (!ModelState.IsValid) return View(reset);

            var user = await _userManager.FindByEmailAsync(reset.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Email ilə bağlanmış hesab tapılmadı !  Doğru emaili daxil etdiyinizdən əmin olun !");
                return View(user);
            }



            var result = await _userManager.ResetPasswordAsync(user, reset.Token, reset.NewPassword);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    return View();
                }
            }
            return RedirectToAction("ResetSuccess", "Account");

        }
        public IActionResult ResetSuccess()
        {
            return View();
        }

        public IActionResult ForgetPass()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPass(ForgetPassViewModel forget)
        {
            if (!ModelState.IsValid) return View(forget);

            var user = await _userManager.FindByEmailAsync(forget.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "Email ilə bağlanmış hesab tapılmadı !  Doğru emaili daxil etdiyinizdən əmin olun !");
                return View(user);
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            string confirmationLink = Url.Action("ResetPass", "Account", new
            {
                email = user.Email,
                token = token
            }, protocol: HttpContext.Request.Scheme);


            var msg = $"<a style=\"text-decoration:none;\" href=\"{confirmationLink}\">Verify Email</a>";

            //SendMailHelper sendEmailHelper = new SendMailHelper();
            bool emailResponse = SendEmail(forget.Email, msg);


            if (emailResponse)
            {
                return RedirectToAction("PassVerification", "Account");
            }

            return View();
        }


        public IActionResult PassVerification()
        {
            return View();
        }


    }
}
