using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ViewModels
{
    public class ProductCreateViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Information { get; set; }
        public int Count { get; set; }
    }
}
