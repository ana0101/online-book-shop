using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.Json.Serialization;

namespace OnlineBookShop.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [JsonIgnore]
        public ICollection<Cart> Carts { get; set; }
    }
}
