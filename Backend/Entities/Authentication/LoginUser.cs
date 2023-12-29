using System.ComponentModel.DataAnnotations;

namespace OnlineBookShop.Entities.Authentication
{
    public class LoginUser
    {
        [Required] public string? Email { get; set; }
        [Required] public string? Password { get; set; }
    }
}
