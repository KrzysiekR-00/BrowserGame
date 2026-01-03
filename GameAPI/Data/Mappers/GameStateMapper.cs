using GameAPI.Domain;
using Shared.State;

namespace GameAPI.Data.Mappers;

internal static class GameStateMapper
{
    internal static GameStateDto ToGameStateDTO(this GameStateContext state)
    {
        GameStateContextDto context = null!;

        switch (state)
        {
            case QuestChoiceContext:
                context = new QuestChoiceContextDto() { Test1 = "" };
                break;
            case CombatContext:
                context = new CombatContextDto() { Test2 = "" };
                break;
            case QuestResultContext:
                context = new QuestResultContextDto() { Test3 = "" };
                break;
            default:
                throw new NotImplementedException();
        }

        var gameState = new GameStateDto
        {
            Type = state.Type,
            Context = context,
            DecisionSlots = state.DecisionSlots
        };

        return gameState;
    }
}
