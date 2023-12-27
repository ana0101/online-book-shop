using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.Json.Serialization;

namespace OnlineBookShop.Models
{
    public class ApplicationUser : IdentityUser
    {
        [JsonIgnore]
        public ICollection<Cart> Carts { get; set; }
    }
}
