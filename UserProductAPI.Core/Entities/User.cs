using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProductAPI.Core.Entities
{
    public class User : IdentityUser<string>
    {
        public string Address { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>(); // Navigation property
    }
}
