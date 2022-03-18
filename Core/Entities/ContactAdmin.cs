using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class ContactAdmin
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime CreateDT { get; set; }


        public string Fullname { get; set; }
    }
}
