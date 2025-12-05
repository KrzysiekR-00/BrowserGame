using GameAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharacterController : ControllerBase
{
    private readonly CharacterService _characterService;

    public CharacterController(CharacterService characterService)
    {
        _characterService = characterService;
    }

    //[Authorize]
    //[HttpGet("profile")]
    //public IActionResult GetProfile()
    //{
    //    var username = User.Identity?.Name;
    //    return Ok(new { Username = username });
    //}
}
