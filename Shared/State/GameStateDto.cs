namespace Shared.State;
public class GameStateDto
{
    public string Type { get; init; } = default!;
    public GameStateContext Context { get; init; } = default!;
    public List<ActionDto> AvailableActions { get; init; } = [];
}
