using GameAPI.Domain;
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

        var state = _gameService.GetState(character.Id);

        GameStateContextDto context = null!;

        switch (state)
        {
            case QuestChoiceContext:
                context = new QuestChoiceContextDto() { Test1 = "Wybierz quest, który chcesz rozpocząć" };
                break;
            case CombatContext:
                context = new CombatContextDto() { Test2 = "Wybierz atak, który chcesz przeprowadzić" };
                break;
            case QuestResultContext:
                context = new QuestResultContextDto() { Test3 = "Quest zakończony" };
                break;
            default:
                throw new NotImplementedException();
        }

        var gameState = new GameStateDto
        {
            Type = state.Type,
            Context = context,
            AvailableActions = state.AvailableActions
        };

        return Ok(gameState);
    }

    [Authorize]
    [HttpPost("decision")]
    public ActionResult<GameStateDto> Decide([FromBody] Guid guid)
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

        var newState = _gameService.ApplyDecision(character.Id, guid);

        GameStateContextDto context = null!;

        switch (newState)
        {
            case QuestChoiceContext:
                context = new QuestChoiceContextDto() { Test1 = "Wybierz quest, który chcesz rozpocząć" };
                break;
            case CombatContext:
                context = new CombatContextDto() { Test2 = "Wybierz atak, który chcesz przeprowadzić" };
                break;
            case QuestResultContext:
                context = new QuestResultContextDto() { Test3 = "Quest zakończony" };
                break;
            default:
                throw new NotImplementedException();
        }

        var gameState = new GameStateDto
        {
            Type = newState.Type,
            Context = context,
            AvailableActions = newState.AvailableActions
        };

        return Ok(gameState);
    }
}
