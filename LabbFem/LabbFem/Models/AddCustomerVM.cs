using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabbFem.Models
{
    public class AddCustomerVM
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string ImageFileName { get; set; }
    }
}
