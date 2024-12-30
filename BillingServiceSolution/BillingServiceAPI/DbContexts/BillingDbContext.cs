using Microsoft.EntityFrameworkCore;

namespace BillingServiceAPI.DbContexts
{
    public class BillingDbContext : DbContext
    {
        public BillingDbContext(DbContextOptions<BillingDbContext> options) : base(options)
        {
        }
        public DbSet<Models.Billing> Billings { get; set; }
        public DbSet<Models.BillingItem> BillingItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Billing>()
                .HasMany(b => b.BillingItems)
                .WithOne(bi => bi.Billing)
                .HasForeignKey(bi => bi.BillingId);
        }
    }

}
