using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public DateTime CreateDT { get; set; }
        public bool IsDeleted { get; set; }

        public List<Product> Products { get; set; }

    }
}
