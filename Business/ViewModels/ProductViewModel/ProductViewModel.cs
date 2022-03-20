using System.Collections.Generic;
using Core.Entities;

namespace Business.ViewModels.ProductViewModel
{
    public class ProductViewModel
    {
        public List<Category> Categories { get; set; }
        public Product Products { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}
