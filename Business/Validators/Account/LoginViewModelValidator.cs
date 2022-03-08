using System;
using System.Collections.Generic;
using System.Text;
using Business.ViewModels.AuthViewModels;
using FluentValidation;

namespace Business.Validators.Account
{
    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(u => u.Email).NotNull().NotEmpty().MaximumLength(255).EmailAddress()
                .WithMessage("Zəhmət olmasa email daxil edin !");
            RuleFor(u => u.Password).NotNull().NotEmpty().MaximumLength(255)
                .WithMessage("Zəhmət olmasa şifrə daxil edin !");
        }
    }
}
