using GameAPI.Domain.Quests;
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
    public abstract GameStateContext Apply(Guid guid);
}

public class QuestChoiceContext : GameStateContext
{
    public override string Type { get; init; }
    public override List<ActionDto> AvailableActions { get; init; }

    [JsonInclude]
    internal Quest[] AvailableQuests { get; init; }

    public override GameStateContext Apply(Guid guid)
    {
        var choosenActionIndex = AvailableActions.IndexOf(AvailableActions.First(a => a.Id == guid));

        var choosenQuest = AvailableQuests[choosenActionIndex];

        return new CombatContext(choosenQuest);
    }

    public QuestChoiceContext()
    {
        Type = string.Empty;
        AvailableActions = [];

        AvailableQuests = new QuestsGenerator().GetAvailableQuests();

        foreach (var quest in AvailableQuests)
        {
            AvailableActions.Add(
                new ActionDto
                {
                    Id = Guid.NewGuid(),
                    Description = $"{quest.Type} - {quest.Location} - {quest.Enemy}"
                }
                );
        }
    }
}
public class CombatContext : GameStateContext
{
    public override string Type { get; init; }
    public override List<ActionDto> AvailableActions { get; init; }

    [JsonInclude]
    internal Quest Quest { get; }

    public override GameStateContext Apply(Guid guid)
    {
        if (AvailableActions.Any(a => a.Id == guid))
        {
            return new QuestResultContext();
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(guid));
        }
    }

    [JsonConstructor]
    internal CombatContext(Quest quest)
    {
        Type = string.Empty;
        AvailableActions = [];

        Quest = quest;

        AvailableActions.Add(new ActionDto { Id = Guid.NewGuid(), Description = "Atak 1" });
        AvailableActions.Add(new ActionDto { Id = Guid.NewGuid(), Description = "Atak 2" });
    }
}
public class QuestResultContext : GameStateContext
{
    public override string Type { get; init; }
    public override List<ActionDto> AvailableActions { get; init; }

    public override GameStateContext Apply(Guid guid)
    {
        if (AvailableActions.Any(a => a.Id == guid))
        {
            return new QuestChoiceContext();
        }
        else
        {
            throw new ArgumentOutOfRangeException(nameof(guid));
        }
    }

    public QuestResultContext()
    {
        Type = string.Empty;
        AvailableActions = [];

        AvailableActions.Add(new ActionDto { Id = Guid.NewGuid(), Description = "Dalej" });
    }
}
