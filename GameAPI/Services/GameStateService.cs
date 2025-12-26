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

    //public GameState ApplyDecision(Guid playerId, GameDecision decision)
    //{
    //    var entity = _db.Set<PlayerStateEntity>()
    //        .Single(x => x.PlayerId == playerId);

    //    var state = GameStateSerializer.Deserialize(entity.StateJson);

    //    var newState = state.Apply(decision);

    //    entity.StateType = newState.Type;
    //    entity.StateJson = GameStateSerializer.Serialize(newState);
    //    entity.StateVersion++;

    //    _db.SaveChanges();

    //    return newState;
    //}
}
