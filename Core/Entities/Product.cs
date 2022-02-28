using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Product:BaseProduct
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Information { get; set; }

    }
}
