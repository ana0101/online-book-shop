using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnlineBookShop.Entities;
using OnlineBookShop.Entities.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace OnlineBookShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthenticationController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
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

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUser loginUser)
        {
            // check if the user exists
            var user = await _userManager.FindByEmailAsync(loginUser.Email);
            if (user == null)
            {
                return Unauthorized("There is no registered user with this email");
            }

            // check the password
            if (!await _userManager.CheckPasswordAsync(user, loginUser.Password))
            {
                return Unauthorized("Incorrect password");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = GetToken(authClaims);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        // register a new user
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUser createUser)
        {
            // check if the user already exists
            var user = await _userManager.FindByEmailAsync(createUser.Email);
            if (user != null)
            {
                return StatusCode(StatusCodes.Status409Conflict,
                    new Response { Status = "Error", Message = "There already exists a registered user with this email" });
            }

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
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Response
                    {
                        Status = "Error",
                        Message = "Error creating user. Please check that the fields are properly filled. " +
                        "The password must contain an uppercase character, lowercase character, a digit, and a non-alphanumeric character and " +
                        "must be at leat six characters long"
                    });
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

            return StatusCode(StatusCodes.Status201Created,
                    new Response { Status = "Success", Message = "User created successfully" });
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] CreateUser createUser)
        {
            // check if the user already exists
            var user = await _userManager.FindByEmailAsync(createUser.Email);
            if (user != null)
            {
                return StatusCode(StatusCodes.Status409Conflict,
                    new Response { Status = "Error", Message = "There already exists a registered user with this email" });
            }

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
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Response { 
                        Status = "Error", 
                        Message = "Error creating user. Please check that the fields are properly filled. " +
                        "The password must contain an uppercase character, lowercase character, a digit, and a non-alphanumeric character and " +
                        "must be at leat six characters long"
                    });
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

            return StatusCode(StatusCodes.Status201Created,
                    new Response { Status = "Success", Message = "Admin created successfully" });
        }
    }
}
