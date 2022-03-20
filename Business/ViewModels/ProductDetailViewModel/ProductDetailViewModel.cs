using System.Collections.Generic;
using Core.Entities;

namespace Business.ViewModels.ProductDetailViewModel
{
    public class ProductDetailViewModel
    {
        public List<Category> Categories { get; set; }
        public Product Products { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<Brand> Brands { get; set; }
    }
}
