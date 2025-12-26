using GameAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.State;

namespace GameAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameStateController : ControllerBase
{
    private readonly UserService _userService;
    private readonly CharacterService _characterService;
    private readonly GameStateService _gameService;

    public GameStateController(UserService userService, CharacterService characterService, GameStateService gameService)
    {
        _userService = userService;
        _characterService = characterService;
        _gameService = gameService;
    }

    [Authorize]
    [HttpGet("gameState")]
    public ActionResult<GameStateDto> GetGameState()
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

        //var gameState = new GameStateDto
        //{
        //    Type = "QUEST_CHOICE",
        //    Context = new QuestChoiceContextDto()
        //};

        //gameState.AvailableActions.Add(new ActionDto { Id = 1, Description = "decision 1" });
        //gameState.AvailableActions.Add(new ActionDto { Id = 2, Description = "decision 2" });

        var state = _gameService.GetState(character.Id);

        var gameState = new GameStateDto
        {
            Type = "QUEST_CHOICE",
            Context = new QuestChoiceContextDto(),
            AvailableActions = state.AvailableActions
        };

        return Ok(gameState);
    }
}
