using Shared.State;
using System.Text.Json.Serialization;

namespace GameAPI.Domain;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "kind")]
[JsonDerivedType(typeof(QuestChoiceContext), "QUEST_CHOICE")]
[JsonDerivedType(typeof(CombatContext), "COMBAT")]
[JsonDerivedType(typeof(QuestResultContext), "QUEST_RESULT")]
public abstract class GameStateContext
{
    public abstract string Type { get; init; }
    //public GameStateContextDto Context { get; init; } = default!;
    public abstract List<ActionDto> AvailableActions { get; init; }
    public abstract GameStateContext Apply(ActionDto decision);
}

public class QuestChoiceContext : GameStateContext
{
    public override string Type { get; init; }
    public override List<ActionDto> AvailableActions { get; init; }

    public override GameStateContext Apply(ActionDto decision)
    {
        return this;
    }

    public QuestChoiceContext()
    {
        Type = string.Empty;
        AvailableActions = new List<ActionDto>();
    }
}
public class CombatContext : GameStateContext
{
    public override string Type { get; init; }
    public override List<ActionDto> AvailableActions { get; init; }

    public override GameStateContext Apply(ActionDto decision)
    {
        return this;
    }

    public CombatContext()
    {
        Type = string.Empty;
        AvailableActions = new List<ActionDto>();
    }
}
public class QuestResultContext : GameStateContext
{
    public override string Type { get; init; }
    public override List<ActionDto> AvailableActions { get; init; }

    public override GameStateContext Apply(ActionDto decision)
    {
        return this;
    }

    public QuestResultContext()
    {
        Type = string.Empty;
        AvailableActions = new List<ActionDto>();
    }
}
