using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Business.ViewModels.ContactAdminViewModels
{
    public class ContactAdminViewModel
    {
        public string Message { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }



        public List<Product> Products { get; set; }
        public List<ProductOperation> ProductOperationsFavourite { get; set; }
        public List<ProductOperation> ProductOperationsSendAndOrder { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}
