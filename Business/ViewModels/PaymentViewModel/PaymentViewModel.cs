using Core.Entities;

namespace Business.ViewModels.PaymentViewModel
{
    public class PaymentViewModel
    {
        public decimal Price { get; set; }
        public Order Order { get; set; }
    }
}
