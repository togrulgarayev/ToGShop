using System;
using Microsoft.AspNetCore.Mvc;
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

        #region Injects

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IContactAdminService _contactAdminService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IProductOperationService _productOperationService;
        private readonly IProductImageService _productImageService;
        private readonly IProductService _productService;

        private readonly ICategoryService _categoryService;

        public AccountController(ICategoryService categoryService,IProductService productService, IProductImageService productImageService, IProductOperationService productOperationService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IContactAdminService contactAdminService, IUnitOfWork unitOfWork, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _contactAdminService = contactAdminService;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;

            _productOperationService = productOperationService;
            _productImageService = productImageService;
            _productService = productService;

            _categoryService = categoryService;
        }

        #endregion


        #region Register

        //Register Operation

        public IActionResult Register()
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Problem", "Error");
            }

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


                
                var msgArea =
                    $"<body style=\"height: 100% !important;margin: 0 !important;padding: 0 !important;width: 100% !important;background-color: #f4f4f4; margin: 0 !important; padding: 0 !important;\"><div style=\"display: none; font-size: 1px; color: #fefefe; line-height: 1px; font-family: \'Lato\', Helvetica, Arial, sans-serif; max-height: 0px; max-width: 0px; opacity: 0; overflow: hidden;\"> Sizinlə əlaqə saxlamaq üçün mesajdır. </div><table style=\"border-collapse: collapse !important;\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td bgcolor=\"#6d6d6d\" align=\"center\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"center\" valign=\"top\" style=\"padding: 40px 10px 40px 10px;\"> </td></tr></table></td></tr><tr><td bgcolor=\"#6d6d6d\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;border-collapse: collapse !important;\"><tr><td bgcolor=\"#ffffff\" align=\"center\" valign=\"top\" style=\"padding: 40px 20px 20px 20px; border-radius: 4px 4px 0px 0px; color: #111111; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 48px; font-weight: 400; letter-spacing: 4px; line-height: 48px;\"><h1 style=\"font-size: 48px; font-weight: 400; margin: 2;\">Xoş Gəlmisiniz</h1> <img src=\"https://www.logomaker.com/api/main/images/1j+ojFVDOMkX9Wytexe43D6khvGJqhNGmBrNwXs1M3EMoAJtliQqgPto9foz\" width=\"125\" height=\"120\" style=\"border: 0;height: auto; line-height: 100%;outline: none; text-decoration: none; display: block; border: 0px;\" /></td></tr></table></td></tr><tr><td bgcolor=\"#f4f4f4\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;border-collapse: collapse !important;\"><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 20px 30px 40px 30px; color: #666666; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0; text-align: center;\">ToG Shopping-də qeydiyyatdan keçdiyiniz üçün təşəkkür edirik. Aşağıdakı keçidə vuraraq hesabınızı aktivləşdirin.</p></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"border-collapse: collapse !important;\"><tr><td bgcolor=\"#ffffff\" align=\"center\" style=\"padding: 20px 30px 60px 30px;\"><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"border-collapse: collapse !important;\"><tr><td align=\"center\" style=\"border-radius: 3px;\" bgcolor=\"#343434\"><a href=\"{confirmationLink}\" target=\"_blank\" style=\"font-size: 20px; font-family: Helvetica, Arial, sans-serif; color: #ffffff; text-decoration: none; color: #ffffff; text-decoration: none; padding: 15px 25px; border-radius: 2px; border: 1px solid #6d6d6d; display: inline-block;\">Doğrula</a></td></tr></table></td></tr></table></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 0px 30px; color: #666666; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0; text-align: center;\">Doğrulama zamanı səhv baş verərsə adminlə əlaqə saxlamağınız xahiş olunur !</p></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 20px 30px 20px 30px; color: #666666; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0; text-align: center;\"><a href=\"mailto:contact.togshop@gmail.com\" target=\"_blank\" style=\"color: #6d6d6d;\">contact.togshop@gmail.com</a></p></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 20px 30px; color: #666666; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0; text-align: center;\">Diqqətiniz üçün çox sağolun !</p></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 40px 30px; border-radius: 0px 0px 4px 4px; color: #666666; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0; text-align: center;\">Hörmətlə: <br>ToG Shopping ©</p></td></tr></table></td></tr><tr><td bgcolor=\"#f4f4f4\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;border-collapse: collapse !important;\"><tr><td bgcolor=\"#f4f4f4\" align=\"left\" style=\"padding: 0px 30px 30px 30px; color: #666666; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 14px; font-weight: 400; line-height: 18px;\"> <br><p style=\"margin: 0; text-align: center;\">© 2022 Bütün Hüquqlar Qorunur | <a href=\"mailto:togrulgarazade@gmail.com\" target=\"_blank\" style=\"color: #111111; font-weight: 700;\"> Togrul Garazade</a>.</p></td></tr></table></td></tr></table></body>";

                var subject = "ToG Shopping - Hesab Doğrulama";

                bool emailResponse = Helper.SendEmail(registerViewModel.Email, msgArea, subject);


                if (emailResponse)
                {
                    await _userManager.AddToRoleAsync(newUser, UserRoles.User.ToString());
                    return RedirectToAction("ConfirmedEmail", "Account");
                }
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
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Problem", "Error");
            }


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


            
            var msg =
                    $"<body style=\"height: 100% !important;margin: 0 !important;padding: 0 !important;width: 100% !important;background-color: #f4f4f4; margin: 0 !important; padding: 0 !important;\"><div style=\"display: none; font-size: 1px; color: #fefefe; line-height: 1px; font-family: \'Lato\', Helvetica, Arial, sans-serif; max-height: 0px; max-width: 0px; opacity: 0; overflow: hidden;\"> Sizinlə əlaqə saxlamaq üçün mesajdır. </div><table style=\"border-collapse: collapse !important;\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\"><tr><td bgcolor=\"#6d6d6d\" align=\"center\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;\"><tr><td align=\"center\" valign=\"top\" style=\"padding: 40px 10px 40px 10px;\"> </td></tr></table></td></tr><tr><td bgcolor=\"#6d6d6d\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;border-collapse: collapse !important;\"><tr><td bgcolor=\"#ffffff\" align=\"center\" valign=\"top\" style=\"padding: 40px 20px 20px 20px; border-radius: 4px 4px 0px 0px; color: #111111; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 48px; font-weight: 400; letter-spacing: 4px; line-height: 48px;\"><h1 style=\"font-size: 48px; font-weight: 400; margin: 2;\">Xoş Gəlmisiniz</h1> <img src=\"https://www.logomaker.com/api/main/images/1j+ojFVDOMkX9Wytexe43D6khvGJqhNGmBrNwXs1M3EMoAJtliQqgPto9foz\" width=\"125\" height=\"120\" style=\"border: 0;height: auto; line-height: 100%;outline: none; text-decoration: none; display: block; border: 0px;\" /></td></tr></table></td></tr><tr><td bgcolor=\"#f4f4f4\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;border-collapse: collapse !important;\"><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 20px 30px 40px 30px; color: #666666; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0; text-align: center;\">ToG Shopping-ə müraciət etdiyiniz üçün təşəkkür edirik. Aşağıdakı keçidə vuraraq şifrəni yeniləyənin siz olduğunuzu təsdiqləyin!</p></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\"><table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"border-collapse: collapse !important;\"><tr><td bgcolor=\"#ffffff\" align=\"center\" style=\"padding: 20px 30px 60px 30px;\"><table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"border-collapse: collapse !important;\"><tr><td align=\"center\" style=\"border-radius: 3px;\" bgcolor=\"#343434\"><a href=\"{confirmationLink}\" target=\"_blank\" style=\"font-size: 20px; font-family: Helvetica, Arial, sans-serif; color: #ffffff; text-decoration: none; color: #ffffff; text-decoration: none; padding: 15px 25px; border-radius: 2px; border: 1px solid #6d6d6d; display: inline-block;\">Təsdiqlə</a></td></tr></table></td></tr></table></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 0px 30px; color: #666666; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0; text-align: center;\">Təsdiqləmə zamanı səhv baş verərsə adminlə əlaqə saxlamağınız xahiş olunur !</p></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 20px 30px 20px 30px; color: #666666; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0; text-align: center;\"><a href=\"mailto:contact.togshop@gmail.com\" target=\"_blank\" style=\"color: #6d6d6d;\">contact.togshop@gmail.com</a></p></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 20px 30px; color: #666666; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0; text-align: center;\">Diqqətiniz üçün çox sağolun !</p></td></tr><tr><td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 0px 30px 40px 30px; border-radius: 0px 0px 4px 4px; color: #666666; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;\"><p style=\"margin: 0; text-align: center;\">Hörmətlə: <br>ToG Shopping ©</p></td></tr></table></td></tr><tr><td bgcolor=\"#f4f4f4\" align=\"center\" style=\"padding: 0px 10px 0px 10px;\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 600px;border-collapse: collapse !important;\"><tr><td bgcolor=\"#f4f4f4\" align=\"left\" style=\"padding: 0px 30px 30px 30px; color: #666666; font-family: \'Lato\', Helvetica, Arial, sans-serif; font-size: 14px; font-weight: 400; line-height: 18px;\"> <br><p style=\"margin: 0; text-align: center;\">© 2022 Bütün Hüquqlar Qorunur | <a href=\"mailto:togrulgarazade@gmail.com\" target=\"_blank\" style=\"color: #111111; font-weight: 700;\"> Togrul Garazade</a>.</p></td></tr></table></td></tr></table></body>";

            var subject = "ToG Shopping - Şifrə yeniləmə";

            //SendMailHelper sendEmailHelper = new SendMailHelper();
            //bool emailResponse = SendEmail(forget.Email, msg);
            bool emailResponse = Helper.SendEmail(forget.Email, msg,subject);



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
        public async Task<IActionResult> UserPage()
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
                ProductImages = productImages,
                Categories = await _categoryService.GetAllAsync()
            };

            return View(contactAdminViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserPage(ContactAdminViewModel contactAdminViewModel)
        {

            ApplicationUser appUser = await _userManager.GetUserAsync(HttpContext.User);

            
            

            await _contactAdminService.Create(contactAdminViewModel, appUser);

            return RedirectToAction(nameof(SendSucces));
        }

        public IActionResult SendSucces()
        {
            return View();
        }

        #endregion


        #region User Settings Page

        [Authorize]
        public async Task<IActionResult> UserSettings()
        {

            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            var userSettingsViewModel = new UserSettingsViewModel()
            {
                Fullname = user.FullName,
                Name = user.Name,
                Surname = user.Surname,
                Phone = user.PhoneNumber,
                Categories = await _categoryService.GetAllAsync(),
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
                    return RedirectToAction("Problem", "Error");
                }

                await _signInManager.RefreshSignInAsync(user);
                return View("ChangePasswordConfirmation");
            }

            return RedirectToAction("Problem", "Error");
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
