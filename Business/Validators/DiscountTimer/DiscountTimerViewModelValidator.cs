using System;
using System.Collections.Generic;
using System.Text;
using Business.ViewModels.DiscountTimerViewModel;
using FluentValidation;

namespace Business.Validators.DiscountTimer
{
    public class DiscountTimerViewModelValidator : AbstractValidator<DiscountTimerViewModel>
    {
        public DiscountTimerViewModelValidator()
        {
            RuleFor(p => p.DiscountTitle).NotNull().NotEmpty().MaximumLength(255).WithMessage("Zəhmət olmasa endirim başlığını daxil edin !");
            RuleFor(p => p.DiscountTime).NotNull().NotEmpty().WithMessage("Zəhmət olmasa endirim tarixi daxil edin !").GreaterThan(DateTime.Now).WithMessage("Keçmiş tarixi daxil edə bilməzsiniz !");
        }
    }
}
