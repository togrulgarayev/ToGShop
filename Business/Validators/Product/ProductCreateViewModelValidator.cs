using Business.ViewModels.ProductViewModels;
using FluentValidation;

namespace Business.Validators.Product
{
    public class ProductCreateViewModelValidator:AbstractValidator<ProductCreateViewModel>
    {
        public ProductCreateViewModelValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(255).WithMessage("Zəhmət olmasa məhsulun adını daxil edin !");
            RuleFor(p => p.Description).NotNull().NotEmpty().MaximumLength(255).WithMessage("Zəhmət olmasa məhsul haqqında məlumat daxil edin !");
            RuleFor(p => p.Price).GreaterThanOrEqualTo(0).WithMessage("Zəhmət olmasa məhsulun qiymətini daxil edin !");
            RuleFor(p => p.Information).NotNull().NotEmpty().MaximumLength(255).WithMessage("Zəhmət olmasa məhsul haqqında əlavə məlumat daxil edin !");
            RuleFor(p => p.Count).GreaterThanOrEqualTo(0).WithMessage("Zəhmət olmasa məhsulun sayını daxil edin !");
            RuleFor(p => p.BrandId).NotNull().NotEmpty().WithMessage("Zəhmət olmasa brendi seçin !");
            RuleFor(p => p.CategoryId).NotNull().NotEmpty().WithMessage("Zəhmət olmasa kateqoriyanı seçin !");
            RuleFor(p => p.ImageFiles).NotNull().NotEmpty().WithMessage("Zəhmət olmasa şəkil(şəkilləri) daxil edin !");
        }
    }
}
