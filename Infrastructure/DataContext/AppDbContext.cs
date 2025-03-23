using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillItem> BillItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<PaymentCard> PaymentCards { get; set; }
        public DbSet<BillPriceLimit> BillPriceLimits { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Bill>()
            //    .HasMany(e => e.BillItems)
            //    .WithOne(e => e.Bill)
            //    .HasForeignKey(e => e.BillId)
            //    .HasPrincipalKey(e => e.Id);

            //modelBuilder.Entity<BillItem>()
            //    .HasOne(b => b.Product)
            //    .WithMany(p => p.BillItems)
            //    .HasForeignKey(bi => bi.ProductId);

            //modelBuilder.Entity<Employee>()
            //    .HasMany(e => e.Bills)
            //    .WithOne(e => e.Employee)
            //    .HasForeignKey(e => e.EmployeeId)
            //    .HasPrincipalKey(e => e.Id);
        }
    }
}
