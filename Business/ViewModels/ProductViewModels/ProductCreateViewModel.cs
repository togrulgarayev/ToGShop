using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Business.ViewModels.ProductViewModels
{
    public class ProductCreateViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Information { get; set; }
        public int Count { get; set; }

        public List<IFormFile> ImageFiles { get; set; }

        public bool IsDiscount { get; set; }
        public decimal DiscountPrice { get; set; }


        public int CategoryId { get; set; }
        public int BrandId { get; set; }





        //public List<Category> Categories { get; set; }
        //public List<Brand> Brands { get; set; }
    }
}
