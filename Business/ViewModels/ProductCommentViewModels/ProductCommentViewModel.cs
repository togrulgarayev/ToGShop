using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Business.ViewModels.ProductCommentViewModels
{
    public class ProductCommentViewModel
    {
        public Core.Entities.Product Product { get; set; }
        public List<Core.Entities.ProductComment> Comments { get; set; }
        public string Username { get; set; }
        public string Comment { get; set; }
        public int ProductId { get; set; }

        public List<Category> Categories { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}
