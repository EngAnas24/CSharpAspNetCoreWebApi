using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyFirstApiProject.Core;

namespace MyFirstApiProject.Data
{
    public class DBData:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-SPO32LR\\SQLEXPRESS;Database=FirstApiApp;Trusted_Connection=True;TrustServerCertificate=True");
        }

     
        public DbSet<Category> categories { get; set; }



    }
}
