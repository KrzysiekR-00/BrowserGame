namespace Shared.Characters;
public class CharacterScheduledTrainingDto
{
    public int AttributeId { get; set; }
    public int AttributeChange { get; set; }
    public DateTime ExecuteAt { get; set; }
}
