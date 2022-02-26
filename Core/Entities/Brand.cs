using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDT { get; set; }
        public bool IsDeleted { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }


        public List<BaseProduct> Products { get; set; }
    }
}
