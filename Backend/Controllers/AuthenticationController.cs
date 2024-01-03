using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineBookShop.Entities;
using OnlineBookShop.Entities.Authentication;
using System.IdentityModel.Tokens.Jwt;

namespace OnlineBookShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly Services.IAuthenticationService _authenticationService;

        public AuthenticationController(UserManager<ApplicationUser> userManager, Services.IAuthenticationService authenticationService)
        {
            _userManager = userManager;
            _authenticationService = authenticationService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await _authenticationService.GetUsers();
            return Ok(users);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUser loginUser)
        {
            // check if the user exists
            var user = await _userManager.FindByEmailAsync(loginUser.Email);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                    new Response { Status = "Error", Message = "There is no registered user with this email" });
            }

            // check the password
            if (!await _userManager.CheckPasswordAsync(user, loginUser.Password))
            {
                return StatusCode(StatusCodes.Status401Unauthorized,
                    new Response { Status = "Error", Message = "Incorrect password" });
            }

            var token = await _authenticationService.Login(user);

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
            ApplicationUser applicationUser = await _authenticationService.RegisterUser(createUser);

            if (applicationUser == null) 
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

            return StatusCode(StatusCodes.Status201Created,
                    new Response { Status = "Success", Message = "User created successfully" });
        }

        [HttpPost]
        [Route("register-admin")]
        [Authorize(Roles = "Admin")]
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
            ApplicationUser applicationUser =await  _authenticationService.RegisterAdmin(createUser);

            if (applicationUser == null)
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

            return StatusCode(StatusCodes.Status201Created,
                    new Response { Status = "Success", Message = "Admin created successfully" });
        }
    }
}
