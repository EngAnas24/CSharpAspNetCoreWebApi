using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CQRS_Lib.Data.Models;
using Microsoft.EntityFrameworkCore;
namespace CQRS_Lib.Data
{
    public class DBData : DbContext
    {
        public DBData(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Items> items { get; set; }
    }
}
