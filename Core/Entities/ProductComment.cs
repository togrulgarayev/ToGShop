using System;

namespace Core.Entities
{
    public class ProductComment
    {
        public int Id { get; set; }
        public DateTime CreateDT { get; set; }
        public string Username { get; set; }
        public string Comment { get; set; }
        public bool IsDelete { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
