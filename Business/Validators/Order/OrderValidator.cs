using FluentValidation;

namespace Business.Validators.Order
{
    public class OrderValidator : AbstractValidator<Core.Entities.Order>
    {
        public OrderValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(255).WithMessage("Zəhmət olmasa adınızı daxil edin !");
            RuleFor(p => p.Surname).NotNull().NotEmpty().WithMessage("Zəhmət olmasa soyadınızı daxil edin !");
            RuleFor(p => p.Address).NotNull().NotEmpty().WithMessage("Zəhmət olmasa ünvanınızı daxil edin !");
            RuleFor(p => p.City).NotNull().NotEmpty().WithMessage("Zəhmət olmasa şəhəri daxil edin !");
            RuleFor(p => p.Number).NotNull().NotEmpty().WithMessage("Zəhmət olmasa telefon nömrəsi daxil edin !");
            RuleFor(p => p.PostalCode).NotNull().NotEmpty().WithMessage("Zəhmət olmasa poçt kodunu daxil edin !");
        }
    }

   
}
