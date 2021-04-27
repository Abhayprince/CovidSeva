using CovidSeva.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace CovidSeva.DataAccess
{
    public class RecordContext : DbContext
    {
        public RecordContext(DbContextOptions<RecordContext> options) : base(options)
        {
        }
        public RecordContext()
        {
        }

        public DbSet<Record> Records { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#if DEBUG
                var connectionString = ConfigurationManager.AppSettings["db_dev"];
#else
                var connectionString = ConfigurationManager.AppSettings["db_prod"];
#endif

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}