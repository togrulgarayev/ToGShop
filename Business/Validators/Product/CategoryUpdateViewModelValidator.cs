using Business.ViewModels.CategoryViewModels;
using FluentValidation;

namespace Business.Validators.Product
{
    public class CategoryUpdateViewModelValidator : AbstractValidator<CategoryUpdateViewModel>
    {
        public CategoryUpdateViewModelValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(255).WithMessage("Zəhmət olmasa kateqoriyanın adını daxil edin !");
        }
    }
}
