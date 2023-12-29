using Microsoft.AspNetCore.Mvc;
using OnlineBookShop.Entities;
using OnlineBookShop.Entities.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OnlineBookShop.Services
{
    public interface IAuthenticationService
    {
        public JwtSecurityToken GetToken(List<Claim> authClaims);
        public Task<JwtSecurityToken> Login(ApplicationUser applicationUser);
        public Task<ApplicationUser?> RegisterUser([FromBody] CreateUser createUser);
        public Task<ApplicationUser?> RegisterAdmin([FromBody] CreateUser createUser);
    }
}
