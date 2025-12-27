using GameAPI.Data;
using GameAPI.Domain;
using System.Text.Json;

namespace GameAPI.Services;

public class GameStateService
{
    private readonly AppDbContext _db;

    public GameStateService(AppDbContext db)
    {
        _db = db;
    }

    public GameStateContext GetState(int characterId)
    {
        var entity = _db.CharactersStates
            .Single(x => x.CharacterId == characterId);

        return JsonSerializer.Deserialize<GameStateContext>(entity.StateJson);
    }

    public GameStateContext ApplyDecision(int characterId, Guid decision)
    {
        var state = GetState(characterId);

        if (!state.AvailableActions.Any(a => a.Id == decision))
        {
            throw new ArgumentOutOfRangeException(nameof(decision));
        }

        var newState = state.Apply(decision);

        var stateEntity = _db.CharactersStates.FirstOrDefault(s => s.CharacterId == characterId);

        stateEntity.StateJson = JsonSerializer.Serialize<GameStateContext>(newState);

        _db.SaveChanges();

        return newState;
    }
}
