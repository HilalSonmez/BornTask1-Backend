using Microsoft.EntityFrameworkCore;
using BornTask1.Models;

namespace BornTask1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
            public DbSet <User> Users{ get; set; } // user tablosu
        public DbSet<FormRecord> FormRecords { get; set; } // form kayıtları tablosu
    }
}
