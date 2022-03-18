using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class DiscountTimer
    {
        public int Id { get; set; }
        public string DiscountTittle { get; set; }
        public DateTime DiscountTime { get; set; }
    }
}
