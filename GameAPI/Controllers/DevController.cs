using GameAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Shared.Accounts;

namespace GameAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DevController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IWebHostEnvironment _env;

    public DevController(AppDbContext db, IWebHostEnvironment env)
    {
        _db = db;
        _env = env;
    }

    [HttpGet("users")]
    public ActionResult<IEnumerable<UserDTO>> Get()
    {
        if (!_env.IsDevelopment())
        {
            return NotFound();
        }

        return Ok(_db.Users.ToArray().Select(u => new UserDTO()
        {
            Username = u.Username,
            PasswordHash = u.PasswordHash
        }));
    }
}
