using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Business.ViewModels.CartViewModel
{
    public class CartViewModel
    {
        public List<Product> Products { get; set; }
        public List<ProductOperation> ProductOperations { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}
