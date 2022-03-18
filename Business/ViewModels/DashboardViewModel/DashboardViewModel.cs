using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Business.ViewModels.DashboardViewModel
{
    public class DashboardViewModel
    {
        public int ProductCount { get; set; }
        public int OrderCount { get; set; }
        public int FavouriteCount { get; set; }
        public int CustomerCount { get; set; }
    }
}
