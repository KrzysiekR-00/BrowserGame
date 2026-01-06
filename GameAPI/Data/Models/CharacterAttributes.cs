namespace GameAPI.Data.Models;

public class CharacterAttribute
{
    public int Id { get; set; }
    public int CharacterId { get; set; }
    public int AttributeId { get; set; }
    public int AttributeValue { get; set; }
}
