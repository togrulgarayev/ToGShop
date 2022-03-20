
using System.Collections.Generic;
using Core.Entities;

namespace Business.ViewModels.DashboardViewModel
{
    public class DashboardViewModel
    {
        public int ProductCount { get; set; }
        public int OrderCount { get; set; }
        public int FavouriteCount { get; set; }
        public int CustomerCount { get; set; }



        public List<Category> Categories { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Product> Products { get; set; }
        public List<ProductImage> ProductImages { get; set; }

        public List<Order> Orders { get; set; }
    }
}
