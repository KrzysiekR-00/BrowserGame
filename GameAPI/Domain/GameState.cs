using Shared.State;

namespace GameAPI.Domain;

//public abstract class GameState
//{
//    public abstract string Type { get; }
//    public abstract IEnumerable<ActionDto> GetAvailableDecisions();
//    public abstract GameState Apply(ActionDto decision);
//}

public class GameState
{
    public string Type { get; init; } = default!;
    public GameStateContextDto Context { get; init; } = default!;
    public List<ActionDto> AvailableActions { get; init; } = [];
    public virtual GameState Apply(ActionDto decision)
    {
        return this;
    }
}

