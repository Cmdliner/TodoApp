using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TodoApp.Services;

namespace TodoApp.Controllers;

[ApiController]
[Route("api/auth")]
public class UserController(IUserService userService, IConfiguration configuration) : ControllerBase
{
    private readonly IUserService _userService = userService;
    private readonly IConfiguration _configuration = configuration;

    private string GenerateJwtToken(User user)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username)
                ]),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw new  Exception(ex.Message);
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        try
        {
            var user = await _userService.RegisterAsync(model.Username, model.Email, model.Password);
            return CreatedAtAction(nameof(Register), new { message = "User regisered successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { mesage = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        try
        {
            var user = await _userService.AuthenticateAsync(model.Email, model.Password);
            //!TODO => Generate JWT token && send in the header
            var token = GenerateJwtToken(user);

            return Ok(new
            {
                user.Id,
                user.Username,
                user.Email,
                Token = token
            });
        }
        catch (Exception)
        {
            return Unauthorized(new { message = "Invalid email or password" });
        }
    }
}


public class RegisterModel
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class LoginModel
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}