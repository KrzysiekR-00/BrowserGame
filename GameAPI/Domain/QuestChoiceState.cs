//using Shared.State;

//namespace GameAPI.Domain;

//public class QuestChoiceState : GameState
//{
//    public override string Type => "QuestChoice";

//    public List<int> AvailableQuestIds { get; init; } = new();

//    public override IEnumerable<ActionDto> GetAvailableDecisions()
//        => AvailableQuestIds.Select(q =>
//            new GameDecision(DecisionType.StartQuest, q.ToString()));

//    public override GameState Apply(ActionDto decision)
//    {
//        if (decision.Type != DecisionType.StartQuest)
//            throw new InvalidGameStateException();

//        return new CombatState
//        {
//            EnemyHp = 10
//        };
//    }
//}

