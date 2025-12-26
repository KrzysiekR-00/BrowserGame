using System.Text.Json.Serialization;

namespace Shared.State;
[JsonPolymorphic(TypeDiscriminatorPropertyName = "kind")]
[JsonDerivedType(typeof(QuestChoiceContextDto), "QUEST_CHOICE")]
[JsonDerivedType(typeof(CombatContextDto), "COMBAT")]
[JsonDerivedType(typeof(QuestResultContextDto), "QUEST_RESULT")]
public abstract class GameStateContextDto
{

}
