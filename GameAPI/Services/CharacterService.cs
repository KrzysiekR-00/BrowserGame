using GameAPI.Data;
using GameAPI.Domain;
using GameAPI.Models;
using Shared.Characters;
using Shared.State;
using System.Text.Json;

namespace GameAPI.Services;

public class CharacterService
{
    private readonly AppDbContext _db;
    private readonly CharacterAttributesService _attributesService;

    public CharacterService(AppDbContext db, IConfiguration config, CharacterAttributesService attributesService)
    {
        _db = db;
        _attributesService = attributesService;
    }

    public void AddCharacter(int userId)
    {
        var character = new Character
        {
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        _db.Characters.Add(character);

        _db.SaveChanges();

        //_db.CharactersAttributes.Add(new CharacterAttributes
        //{
        //    CharacterId = character.Id,
        //    AttributesCsv = new Shared.Characters.Attributes(1, 2, 3).ToCsv()
        //});

        //_db.SaveChanges();

        var initialState = new QuestChoiceContext()
        {
            Type = "test1",
            AvailableActions = new List<Shared.State.ActionDto>()
        };
        initialState.AvailableActions.Add(new ActionDto { Id = Guid.NewGuid(), Description = "decision 1" });
        initialState.AvailableActions.Add(new ActionDto { Id = Guid.NewGuid(), Description = "decision 2" });
        initialState.AvailableActions.Add(new ActionDto { Id = Guid.NewGuid(), Description = "decision 3" });

        CharacterState state = new CharacterState();
        state.CharacterId = character.Id;
        state.StateJson = JsonSerializer.Serialize<GameStateContext>(initialState);

        _db.CharactersStates.Add(state);
        _db.SaveChanges();

        //

        var dto = new AttributesDto();
        _attributesService.SaveAttributes(dto, character.Id);
    }

    public Character GetByUserId(int userId)
    {
        return _db.Characters.FirstOrDefault(c => c.UserId == userId);
    }
}
