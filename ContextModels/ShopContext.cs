using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineBookShop.Models;

namespace OnlineBookShop.ContextModels
{
    public class ShopContext : IdentityDbContext<IdentityUser>
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
