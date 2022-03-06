using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ViewModels.FavouriteViewModels
{
    public class FavouriteItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public bool isDiscount { get; set; }
        public decimal DiscountPrice { get; set; }
    }
}
