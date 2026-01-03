namespace Shared.State;
public class GameStateDto
{
    public string Type { get; init; } = default!;
    public GameStateContextDto Context { get; init; } = default!;
    public List<DecisionSlotDto> DecisionSlots { get; init; } = [];
}

public class DecisionSlotDto
{
    public Guid Id { get; init; } = default!;
    public string Description { get; init; } = default!;
    public List<ActionDto> AvailableActions { get; init; } = [];
}
