namespace GameAPI.Models;

public class ScheduledAttributeChange
{
    public int Id { get; set; }
    public int CharacterId { get; set; }
    public int AttributeId { get; set; }
    public int AttributeChange { get; set; }
    public DateTime ExecuteAt { get; set; }
}
