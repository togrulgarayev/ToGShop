﻿using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Business.ViewModels.UserOrderViewModel
{
    public class UserOrderViewModel
    {
        public List<Product> Products { get; set; }
        public List<ProductOperation> ProductOperationsOrders { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}
