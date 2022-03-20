using System.Collections.Generic;
using Core.Entities;

namespace Business.ViewModels.CategoriesViewModel
{
    public class CategoriesViewModel
    {

        public List<Category> Categories { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Product> Products { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}
