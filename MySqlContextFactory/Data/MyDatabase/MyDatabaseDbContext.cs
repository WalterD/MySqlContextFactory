using Microsoft.EntityFrameworkCore;
using MySqlContextFactory.Data.MyDatabase.Models;

namespace MySqlContextFactory.Data.MyDatabase
{
    public class MyDatabaseDbContext: DbContext
    {
        public MyDatabaseDbContext(DbContextOptions<MyDatabaseDbContext> options) : base(options)
        {
            this.ChangeTracker.AutoDetectChangesEnabled = false;
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }
    }
}
