using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<Bill> Bills { get; set; } = null!;
        public DbSet<BillItem> BillItems { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<PaymentCard> PaymentCards { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bill>()
                .HasMany(e => e.BillItems)
                .WithOne(e => e.Bill)
                .HasForeignKey(e => e.BillId);

            modelBuilder.Entity<BillItem>()
                .HasOne(b => b.Product)
                .WithMany(p => p.BillItems)
                .HasForeignKey(bi => bi.ProductId);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Bills)
                .WithOne(e => e.Employee)
                .HasForeignKey(e => e.EmployeeId);
        }
    }
}
