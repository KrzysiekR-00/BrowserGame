using GameAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Characters;

namespace GameAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttributesController : ControllerBase
{
    private readonly CharacterAttributesService _attributesService;
    private readonly UserService _userService;
    private readonly CharacterService _characterService;

    public AttributesController(CharacterAttributesService attributesService, UserService userService, CharacterService characterService)
    {
        _attributesService = attributesService;
        _userService = userService;
        _characterService = characterService;
    }

    [Authorize]
    [HttpGet("attributes")]
    public ActionResult<AttributesDTO> GetAttributes()
    {
        var username = User.Identity?.Name;
        var user = _userService.GetByUsername(username);

        if (user == null)
        {
            return Problem("User not found");
        }

        var character = _characterService.GetByUserId(user.Id);

        if (character == null)
        {
            return Problem("Character not found");
        }

        var attributes = _attributesService.GetAttributes(character.Id);

        return Ok(attributes);
    }
}
