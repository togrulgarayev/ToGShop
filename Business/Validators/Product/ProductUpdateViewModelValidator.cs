using System;
using System.Collections.Generic;
using System.Text;
using Business.ViewModels.ProductViewModels;
using FluentValidation;

namespace Business.Validators.Product
{
    public class ProductUpdateViewModelValidator : AbstractValidator<ProductUpdateViewModel>
    {
        public ProductUpdateViewModelValidator()
        {
            RuleFor(p => p.Name).MaximumLength(255);
            RuleFor(p => p.Description).MaximumLength(255);
            RuleFor(p => p.Price).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Information).MaximumLength(255);
            RuleFor(p => p.Count).GreaterThanOrEqualTo(0);
        }
    }
}
