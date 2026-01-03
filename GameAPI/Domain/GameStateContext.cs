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
    public abstract List<DecisionSlotDto> DecisionSlots { get; init; }
    public abstract GameStateContext Apply(Dictionary<Guid, Guid> chosenActions);
}

public class QuestChoiceContext : GameStateContext
{
    public override string Type { get; init; }
    public override List<DecisionSlotDto> DecisionSlots { get; init; }

    [JsonInclude]
    internal Quest[] AvailableQuests { get; init; }

    public override GameStateContext Apply(Dictionary<Guid, Guid> chosenActions)
    {
        var chosenAction = chosenActions.First();

        var availableActions = DecisionSlots
            .First(d => d.Id == chosenAction.Key)
            .AvailableActions;

        var choosenActionIndex = availableActions
            .IndexOf(availableActions.First(a => a.Id == chosenAction.Value));

        var choosenQuest = AvailableQuests[choosenActionIndex];

        return new CombatContext(choosenQuest);
    }

    public QuestChoiceContext()
    {
        Type = string.Empty;

        List<ActionDto> AvailableActions = [];

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

        DecisionSlots = [];
        DecisionSlots.Add(new DecisionSlotDto()
        {
            Id = Guid.NewGuid(),
            Description = "Wybierz quest, który chcesz rozpocząć",
            AvailableActions = AvailableActions
        });
    }
}
public class CombatContext : GameStateContext
{
    public override string Type { get; init; }
    public override List<DecisionSlotDto> DecisionSlots { get; init; }

    [JsonInclude]
    internal Quest Quest { get; }

    public override GameStateContext Apply(Dictionary<Guid, Guid> chosenActions)
    {
        return new QuestResultContext();
    }

    [JsonConstructor]
    internal CombatContext(Quest quest)
    {
        Quest = quest;

        Type = string.Empty;

        List<ActionDto> AvailableActions = [];
        AvailableActions.Add(new ActionDto { Id = Guid.NewGuid(), Description = "Atak 1" });
        AvailableActions.Add(new ActionDto { Id = Guid.NewGuid(), Description = "Atak 2" });

        DecisionSlots = [];
        DecisionSlots.Add(new DecisionSlotDto()
        {
            Id = Guid.NewGuid(),
            Description = "Wybierz atak, który chcesz przeprowadzić",
            AvailableActions = AvailableActions
        });
    }
}
public class QuestResultContext : GameStateContext
{
    public override string Type { get; init; }
    public override List<DecisionSlotDto> DecisionSlots { get; init; }

    public override GameStateContext Apply(Dictionary<Guid, Guid> chosenActions)
    {
        return new QuestChoiceContext();
    }

    public QuestResultContext()
    {
        Type = string.Empty;
        List<ActionDto> AvailableActions = [];

        AvailableActions.Add(new ActionDto { Id = Guid.NewGuid(), Description = "Dalej" });

        DecisionSlots = [];
        DecisionSlots.Add(new DecisionSlotDto()
        {
            Id = Guid.NewGuid(),
            Description = "Quest zakończony",
            AvailableActions = AvailableActions
        });
    }
}
