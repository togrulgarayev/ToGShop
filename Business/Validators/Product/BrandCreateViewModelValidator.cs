using System;
using System.Collections.Generic;
using System.Text;
using Business.ViewModels.BrandViewModels;
using FluentValidation;

namespace Business.Validators.Product
{

    public class BrandCreateViewModelValidator : AbstractValidator<BrandCreateViewModel>
    {
        public BrandCreateViewModelValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(255).WithMessage("Zəhmət olmasa brendin adını daxil edin !");
            RuleFor(p => p.Logo).NotNull().NotEmpty().WithMessage("Zəhmət olmasa şəkil daxil edin !");
        }
    }
}
