using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDT { get; set; }
        public bool IsDeleted { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string Information { get; set; }
        public List<ProductImage> ProductImages { get; set; }

        public bool IsDiscount { get; set; }
        public decimal DiscountPrice { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

    }
}
