using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProductAPI.Core.Entities;

namespace UserProductAPI.Infrastructure.Data
{
    public class UserProductAuthDbContext : IdentityDbContext<User, IdentityRole<string>, string>
    {
        public UserProductAuthDbContext(DbContextOptions<UserProductAuthDbContext> options) : base(options) 
        {

            
        }
        public DbSet<User> Users { get; set; }
    }
}
