using GameAPI.Data;
using GameAPI.Domain;
using Shared.State;
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

    public GameStateContext ApplyDecision(int characterId, DecisionRequestDto decisionRequestDto)
    {
        var state = GetState(characterId);

        var hasAllRequiredSlots = state.DecisionSlots.All(d => decisionRequestDto.ChosenActions.ContainsKey(d.Id));
        var allDecisionsValid = decisionRequestDto.ChosenActions.All(a => state.DecisionSlots.FirstOrDefault(d => d.Id == a.Key)?.AvailableActions.Any(aa => aa.Id == a.Value) == true);

        if (!hasAllRequiredSlots || !allDecisionsValid)
        {
            throw new ArgumentOutOfRangeException(nameof(decisionRequestDto));
        }

        var newState = state.Apply(decisionRequestDto.ChosenActions);

        var stateEntity = _db.CharactersStates.FirstOrDefault(s => s.CharacterId == characterId);

        stateEntity.StateJson = JsonSerializer.Serialize<GameStateContext>(newState);

        _db.SaveChanges();

        return newState;
    }
}
