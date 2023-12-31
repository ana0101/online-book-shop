using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineBookShop.Entities;

namespace OnlineBookShop.ContextModels
{
    public class ShopContext : IdentityDbContext<IdentityUser>
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>().HasKey(c => new { c.ApplicationUserId, c.BookId });
            modelBuilder.Entity<BookOrder>().HasKey(bo => new { bo.OrderId, bo.BookId });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<BookOrder> BookOrders { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }
    }
}
