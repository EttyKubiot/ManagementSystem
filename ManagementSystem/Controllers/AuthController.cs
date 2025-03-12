using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ManagementSystem.Data.Entities;
using ManagementSystem.Services;
using Microsoft.AspNetCore.Identity;


namespace ManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthController(UserService userService, IConfiguration configuration, IPasswordHasher<User> passwordHasher)
        {
            _userService = userService;
            _configuration = configuration;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            var user = await _userService.GetUserByNameAsync(loginModel.FirstName, loginModel.LastName);
            if (user == null)
                return Unauthorized(new { message = "User not found" });

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginModel.Password);
            if (result == PasswordVerificationResult.Failed)
                return Unauthorized(new { message = "Invalid password" });

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
             new Claim(ClaimTypes.Role, user.Role.Name)
        };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new { Token = tokenString });
        }
    }
}
