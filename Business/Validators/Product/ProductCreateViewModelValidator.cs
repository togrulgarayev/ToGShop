using System;
using System.Collections.Generic;
using System.Text;
using Business.ViewModels;
using FluentValidation;

namespace Business.Validators.Product
{
    class ProductCreateViewModelValidator:AbstractValidator<ProductCreateViewModel>
    {
        public ProductCreateViewModelValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(255);
            RuleFor(p => p.Description).NotNull().NotEmpty().MaximumLength(255);
            RuleFor(p => p.Price).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Information).NotNull().NotEmpty().MaximumLength(255);
            RuleFor(p => p.Count).GreaterThanOrEqualTo(0);
            RuleFor(p => p.BrandId).NotNull().NotEmpty();
            RuleFor(p => p.CategoryId).NotNull().NotEmpty();
        }
    }
}
