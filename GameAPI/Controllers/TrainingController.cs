using GameAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Characters;

namespace GameAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainingController : ControllerBase
{
    private readonly UserService _userService;
    private readonly CharacterService _characterService;
    private readonly TrainingService _trainingService;

    public TrainingController(UserService userService, CharacterService characterService, TrainingService trainingService)
    {
        _userService = userService;
        _characterService = characterService;
        _trainingService = trainingService;
    }

    [Authorize]
    [HttpPost("train")]
    public ActionResult<CharacterScheduledTraining> Train([FromBody] Shared.Characters.Attribute attribute)
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

        var scheduled = _trainingService.StartTraining(attribute, character.Id);

        return Ok(scheduled);
    }

    [Authorize]
    [HttpGet("scheduledTraining")]
    public ActionResult<CharacterScheduledTraining[]> ScheduledTraining()
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

        var scheduled = _trainingService.GetScheduledTraining(character.Id);

        return Ok(scheduled);
    }
}
