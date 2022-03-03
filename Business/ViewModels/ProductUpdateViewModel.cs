using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ViewModels
{
    public class ProductUpdateViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}
