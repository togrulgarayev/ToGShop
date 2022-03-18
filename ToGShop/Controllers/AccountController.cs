using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Utilities;
using Business.ViewModels.AuthViewModels;
using Business.ViewModels.ContactAdminViewModels;
using Business.ViewModels.UserSettingsViewModel;
using Core;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ToGShop.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IContactAdminService _contactAdminService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IProductOperationService _productOperationService;
        private readonly IProductImageService _productImageService;
        private readonly IProductService _productService;

        public AccountController(IProductService productService,IProductImageService productImageService,IProductOperationService productOperationService,UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager, IContactAdminService contactAdminService, IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _contactAdminService = contactAdminService;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;

            _productOperationService = productOperationService;
            _productImageService = productImageService;
            _productService = productService;
        }


        #region Register

        //Register Operation

        public IActionResult Register()
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





                //bool emailResponse = SendEmail(registerViewModel.Email, msgArea);
                bool emailResponse = Helper.SendEmail(registerViewModel.Email, msgArea);


                if (emailResponse)
                    await _userManager.AddToRoleAsync(newUser, UserRoles.User.ToString());
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

        //Register Operation - End

        #endregion


        #region Login

        //Login Operation

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string ReturnUrl)
        {

            if (!ModelState.IsValid) return View(loginViewModel);
            ApplicationUser user = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if (user == null)
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
                ModelState.AddModelError(string.Empty, "Zəhmət olmasa bir neçə dəqiqə gözləyin !");
                return View(loginViewModel);
            }


            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Email və ya şifrə yanlışdır !");
                return View(loginViewModel);
            }


            if (ReturnUrl != null)
            {

                return Redirect(ReturnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        //Login Operation - End

        #endregion


        #region Logout

        //Logout Operation

        public async Task<IActionResult> Logout(string ReturnUrl)
        {
            await _signInManager.SignOutAsync();

            if (ReturnUrl != null)
            {

                return Redirect(ReturnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        //Logout Operation - End

        #endregion


        #region Confirmation user email on register

        //Registration Success
        public ActionResult ConfirmedEmail()
        {
            return View();
        }

        //Confirmation Success
        public ActionResult ConfirmationEmail()
        {
            return View();
        }

        //Confirm Email Operation

        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound();

            var result = await _userManager.ConfirmEmailAsync(user, token);
            return View(result.Succeeded ? "ConfirmationEmail" : "Error");
        }

        //Confirm Email Operation - End

        #endregion


        #region Forget password and reset password


        //Reset password
        public IActionResult ResetPass(string token, string email)
        {
            if (token == null && email == null)
            {
                ModelState.AddModelError("", "Axtardığınız email ilə bağlanmış hesab yoxdur ! ");
            }

            return View();
        }

        //Reset password operation
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
        //Reset password operation - End

        //Reset password succesful
        public IActionResult ResetSuccess()
        {
            return View();
        }

        //Forget password
        public IActionResult ForgetPass()
        {
            return View();
        }

        //Forget password operation
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


            //var msg = $"<a style=\"text-decoration:none;\" href=\"{confirmationLink}\">Verify Email</a>";

            var msg =
                $"<div style=\"border:2px solid #a2a2a2; border-radius:8px; text-align:center;\"><h3 style=\"background:#a2a2a2; color:white; border: 2px solid #a2a2a2; border-radius:8px;text-align:center;\">ToG Shopping - Email Təsdiqləmə</h3><p>Sizi ToG Shopping-də gördüyümüz üçün şad olduq ! Aşağıdakı keçid linkinə tıklayın və emailinizi təsdiqləyərək hesabınızı aktivləşdirin! Diqqətiniz üçün təşəkkürlər :) </p><br><a style=\"text-decoration:none;\" href=\"{confirmationLink}\">Təsdiqlə</a></div>";


            //SendMailHelper sendEmailHelper = new SendMailHelper();
            //bool emailResponse = SendEmail(forget.Email, msg);
            bool emailResponse = Helper.SendEmail(forget.Email, msg);



            if (emailResponse)
            {
                return RedirectToAction("PassVerification", "Account");
            }

            return View();
        }
        //Forget password operation - End

        //Send email for reset forget account password successful
        public IActionResult PassVerification()
        {
            return View();
        }


        #endregion


        #region Send Email

        //Send Email Operation

        //public bool SendEmail(string userEmail, string msgArea)
        //{
        //    MailMessage mailMessage = new MailMessage();
        //    mailMessage.From = new MailAddress("contact.togshop@gmail.com");
        //    mailMessage.To.Add(new MailAddress(userEmail));

        //    mailMessage.Subject = "Email Təsdiq Mesajı - ToG Shopping";
        //    mailMessage.IsBodyHtml = true;
        //    mailMessage.Body = msgArea;

        //    SmtpClient client = new SmtpClient();
        //    client.Credentials = new NetworkCredential("contact.togshop@gmail.com", "ohtiqxjdkojlpqez");
        //    client.Host = "smtp.gmail.com";
        //    client.Port = 587;
        //    client.EnableSsl = true;
        //    try
        //    {
        //        client.Send(mailMessage);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        // log exception
        //    }
        //    return false;
        //}

        //Send Email Operation - End

        #endregion


        #region External login register

        //Login with Facebook

        public IActionResult FacebookLogin(string returnUrl)
        {
            string redirectUrl = Url.Action("SocialMediaResponse", "Account", new {returnUrl = returnUrl});
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Facebook", redirectUrl);
            return new ChallengeResult("Facebook", properties);
        }

        //Login with Facebook - End

        //Login with Google

        public IActionResult GoogleLogin(string returnUrl)
        {
            string redirectUrl = Url.Action("SocialMediaResponse", "Account", new { returnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
            return new ChallengeResult("Google", properties);
        }

        //Login with Google - End



        // Social network login/register operation

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

                        await _userManager.AddToRoleAsync(user, UserRoles.User.ToString());

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

        // Social network login/register operation - End


        #endregion




        #region User Page

        [Authorize]
        public async Task<IActionResult> User()
        {

            ApplicationUser appUser = await _userManager.GetUserAsync(HttpContext.User);
            var products = await _productService.GetAllAsync();
            var productOperationsFavourite = await _productOperationService.GetAllFavouriteAsync(appUser.Id);
            var productOperationsOrderedSend = await _productOperationService.GetAllOrderedSendAsync(appUser.Id);
            var productImages = await _productImageService.GetAllAsync();

            var contactAdminViewModel = new ContactAdminViewModel()
            {
                Fullname = appUser.FullName,
                Username = appUser.UserName,
                Email = appUser.Email,
                Name = appUser.Name,
                Surname = appUser.Surname,
                Phone = appUser.PhoneNumber,
                Products = products,
                ProductOperationsFavourite = productOperationsFavourite,
                ProductOperationsSendAndOrder = productOperationsOrderedSend,
                ProductImages = productImages
            };

            return View(contactAdminViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> User(ContactAdminViewModel contactAdminViewModel)
        {

            ApplicationUser appUser = await _userManager.GetUserAsync(HttpContext.User);

            
            

            await _contactAdminService.Create(contactAdminViewModel, appUser);

            return RedirectToAction(nameof(SendSucces));
        }

        public IActionResult SendSucces()
        {
            return View();
        }


        [Authorize]
        public async Task<IActionResult> UserSettings()
        {

            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            var userSettingsViewModel = new UserSettingsViewModel()
            {
                Fullname = user.FullName,
                Name = user.Name,
                Surname = user.Surname,
                Phone = user.PhoneNumber
            };

            return View(userSettingsViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(UserSettingsViewModel userSettingsViewModel)
        {
            if (ModelState.IsValid)
            {
                
                ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);


                var result = await _userManager.ChangePasswordAsync(user,
                    userSettingsViewModel.CurrentPassword, userSettingsViewModel.NewPassword);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return RedirectToAction("Problem","Error");
                }

                await _signInManager.RefreshSignInAsync(user);
                return View("ChangePasswordConfirmation");
            }

            return RedirectToAction("Problem","Error");
        }

        public IActionResult ChangePasswordConfirmation()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeUserData(UserSettingsViewModel userSettingsViewModel)
        {

            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);




            user.Name = userSettingsViewModel.Name;
            user.Surname = userSettingsViewModel.Surname;
            user.PhoneNumber = userSettingsViewModel.Phone;
            user.FullName = userSettingsViewModel.Fullname;

            await _userManager.UpdateAsync(user);

            return RedirectToAction("UserSettings");
        }


        #endregion






        #region for create roles

        //public async Task CreateRole()
        //{
        //    foreach (var role in Enum.GetValues(typeof(UserRoles)))
        //    {
        //        if (!await _roleManager.RoleExistsAsync(role.ToString()))
        //        {
        //            await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
        //        }
        //    }
        //}

        #endregion



























    }
}
