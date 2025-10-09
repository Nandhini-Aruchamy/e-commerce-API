using System.Collections.Generic;
using e_commerce_API.Model.Products;
using e_commerce_API.Model.Users;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_API.Data
{
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options)
        {
        }
        //public DbSet<Login> Logins { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
