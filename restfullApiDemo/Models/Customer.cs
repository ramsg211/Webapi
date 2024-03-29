using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restfullApiDemo.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string LoginUser { get; set; }
    }
}
