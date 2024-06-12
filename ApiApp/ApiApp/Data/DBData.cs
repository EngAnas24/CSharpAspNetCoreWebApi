using ApiApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace ApiApp.Data
{
    public class DBData:IdentityDbContext<AppUser>
    {
        public DBData(DbContextOptions<DBData> options) : base(options)
        {
        }


         public DbSet<Category> categories { get; set; }
         public DbSet<Item> items { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }

    }
}
