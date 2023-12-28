using System.ComponentModel.DataAnnotations;

namespace OnlineBookShop.Entities.Authentication
{
    public class CreateUser
    {
        [Required] public string? FirstName { get; set; }
        [Required] public string? LastName { get; set; }
        [Required] public string? UserName { get; set; }
        [Required] public string? Email { get; set; }
        [Required] public string? Password { get; set; }
    }
}
