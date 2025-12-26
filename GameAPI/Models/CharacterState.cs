namespace GameAPI.Models;

public class CharacterState
{
    public int Id { get; set; }
    public int CharacterId { get; set; }
    public string StateType { get; set; } = string.Empty;
    public string StateJson { get; set; } = string.Empty;
}
