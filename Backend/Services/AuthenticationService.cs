using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineBookShop.ContextModels;
using OnlineBookShop.Entities;
using OnlineBookShop.Entities.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineBookShop.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ShopContext _shopContext;

        public AuthenticationService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ShopContext shopContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _shopContext = shopContext;
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            var users = await _shopContext.ApplicationUsers.ToListAsync();
            return users;
        }

        public JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        public async Task<JwtSecurityToken> Login(ApplicationUser applicationUser)
        {
            var roles = await _userManager.GetRolesAsync(applicationUser);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, applicationUser.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = GetToken(authClaims);

            return token;
        }

        public async Task<ApplicationUser?> RegisterUser(CreateUser createUser)
        {
            // create the user
            ApplicationUser applicationUser = new()
            {
                FirstName = createUser.FirstName,
                LastName = createUser.LastName,
                UserName = createUser.UserName,
                Email = createUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var newUser = await _userManager.CreateAsync(applicationUser, createUser.Password);
            if (!newUser.Succeeded)
            {
                return null;
            }

            // check if the role exists
            if (!await _roleManager.RoleExistsAsync(Roles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(Roles.User));
            }

            // assign role
            if (await _roleManager.RoleExistsAsync(Roles.User))
            {
                await _userManager.AddToRoleAsync(applicationUser, Roles.User);
            }

            return applicationUser;
        }

        public async Task<ApplicationUser?> RegisterAdmin(CreateUser createUser)
        {
            // create the user
            ApplicationUser applicationUser = new()
            {
                FirstName = createUser.FirstName,
                LastName = createUser.LastName,
                UserName = createUser.UserName,
                Email = createUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var newUser = await _userManager.CreateAsync(applicationUser, createUser.Password);

            if (!newUser.Succeeded)
            {
                return null;
            }

            // check if the roles exist
            if (!await _roleManager.RoleExistsAsync(Roles.User))
            {
                await _roleManager.CreateAsync(new IdentityRole(Roles.User));
            }
            if (!await _roleManager.RoleExistsAsync(Roles.Admin))
            {
                await _roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }

            // assign roles
            if (await _roleManager.RoleExistsAsync(Roles.User))
            {
                await _userManager.AddToRoleAsync(applicationUser, Roles.User);
            }
            if (await _roleManager.RoleExistsAsync(Roles.Admin))
            {
                await _userManager.AddToRoleAsync(applicationUser, Roles.Admin);
            }

            return applicationUser;
        }
    }
}
