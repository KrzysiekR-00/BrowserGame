using GameAPI.Data;
using GameAPI.Data.Models;
using GameAPI.Domain;
using Shared.Characters;
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

        var initialState = new QuestChoiceContext()
        {
            Type = "test1",
        };

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
