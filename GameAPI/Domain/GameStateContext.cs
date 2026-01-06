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

        AvailableQuests = new QuestsGenerator().GetAvailableQuests();

        List<ActionDto> availableActions = [];
        foreach (var quest in AvailableQuests)
        {
            availableActions.Add(
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
            AvailableActions = availableActions
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

        List<ActionDto> availableMovementActions = [];
        availableMovementActions.Add(new ActionDto { Id = Guid.NewGuid(), Description = "Ruch 1" });
        availableMovementActions.Add(new ActionDto { Id = Guid.NewGuid(), Description = "Ruch 2" });
        availableMovementActions.Add(new ActionDto { Id = Guid.NewGuid(), Description = "Ruch 3" });

        List<ActionDto> availableAttackActions = [];
        availableAttackActions.Add(new ActionDto { Id = Guid.NewGuid(), Description = "Atak 1" });
        availableAttackActions.Add(new ActionDto { Id = Guid.NewGuid(), Description = "Atak 2" });
        availableAttackActions.Add(new ActionDto { Id = Guid.NewGuid(), Description = "Atak 3" });

        DecisionSlots = [];
        DecisionSlots.Add(new DecisionSlotDto()
        {
            Id = Guid.NewGuid(),
            Description = "Wybierz atak, który chcesz przeprowadzić",
            AvailableActions = availableAttackActions
        });
        DecisionSlots.Add(new DecisionSlotDto()
        {
            Id = Guid.NewGuid(),
            Description = "Wybierz ruch, który chcesz wykonać",
            AvailableActions = availableMovementActions
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

        List<ActionDto> availableActions = [];
        availableActions.Add(new ActionDto { Id = Guid.NewGuid(), Description = "Dalej" });

        DecisionSlots = [];
        DecisionSlots.Add(new DecisionSlotDto()
        {
            Id = Guid.NewGuid(),
            Description = "Quest zakończony",
            AvailableActions = availableActions
        });
    }
}
