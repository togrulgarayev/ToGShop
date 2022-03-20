using System.Collections.Generic;
using Core.Entities;

namespace Business.ViewModels.UserSendViewModel
{
    public class UserSendViewModel
    {
        public List<Product> Products { get; set; }
        public List<ProductOperation> ProductOperationsOrders { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}
