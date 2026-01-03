namespace Shared.State;
public class DecisionRequestDto
{
    public Dictionary<Guid, Guid> ChosenActions { get; init; } = [];
}
