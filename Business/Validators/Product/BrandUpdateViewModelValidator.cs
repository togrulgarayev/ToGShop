using Business.ViewModels.BrandViewModels;
using FluentValidation;

namespace Business.Validators.Product
{
    public class BrandUpdateViewModelValidator : AbstractValidator<BrandUpdateViewModel>
    {
        public BrandUpdateViewModelValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(255).WithMessage("Zəhmət olmasa brendin adını daxil edin !");
        }
    }
}
