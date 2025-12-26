using GameAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared.Accounts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GameAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;
    private readonly CharacterService _characterService;
    private readonly IConfiguration _config;

    public AuthController(UserService userService, IConfiguration config, CharacterService characterService)
    {
        _userService = userService;
        _config = config;
        _characterService = characterService;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            return BadRequest("Login i hasło są wymagane.");

        if (request.Password != request.ConfirmPassword)
            return BadRequest("Hasła nie są takie same.");

        if (_userService.UserExists(request.Username))
            return Conflict("Użytkownik o tej nazwie już istnieje.");

        _userService.AddUser(request.Username, request.Password);

        var user = _userService.GetByUsername(request.Username);
        if (user == null)
        {
            return Problem("User not found");
        }
        _characterService.AddCharacter(user.Id);

        return Ok("Konto utworzone pomyślnie.");
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequestDto request)
    {
        var user = _userService.GetByUsername(request.Username);
        if (user == null || !_userService.VerifyPassword(user, request.Password))
            return Unauthorized("Invalid username or password.");

        var token = GenerateJwtToken(user.Username);
        return Ok(new { token });
    }

    [Authorize]
    [HttpGet("profile")]
    public IActionResult GetProfile()
    {
        var username = User.Identity?.Name;
        return Ok(new { Username = username });
    }

    private string GenerateJwtToken(string username)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username)
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    [HttpGet]
    public IEnumerable<UserDTO> Get()
    {
        return _userService.GetUsers().Select(u => new UserDTO()
        {
            Username = u.Username,
            PasswordHash = u.PasswordHash
        });
    }
}