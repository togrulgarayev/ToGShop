using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities
{
    public class ProductOperation
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }


        public bool IsDeleted { get; set; }
        public bool IsFavourite { get; set; }
        public bool InCart { get; set; }
        public bool IsOrdered { get; set; }
        public bool IsSend { get; set; }
    }
}
