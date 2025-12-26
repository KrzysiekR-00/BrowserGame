//namespace GameAPI.Domain;

//public class CombatState : GameState
//{
//    public override string Type => "Combat";

//    public int EnemyHp { get; set; }

//    public override IEnumerable<GameDecision> GetAvailableDecisions()
//        => new[]
//        {
//            new GameDecision(DecisionType.CombatAction, "ATTACK")
//        };

//    public override GameState Apply(GameDecision decision)
//    {
//        if (decision.Type != DecisionType.CombatAction)
//            throw new InvalidGameStateException();

//        EnemyHp -= 3;

//        if (EnemyHp <= 0)
//            return new QuestResultState { Success = true };

//        return this;
//    }
//}

