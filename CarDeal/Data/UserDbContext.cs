using CarDeal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDeal.Data
{
    public class UserDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Post> Posts { get; set; }
        public UserDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
