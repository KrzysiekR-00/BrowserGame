namespace Shared.Characters;

public class AttributesDto
{
    public Dictionary<AttributeType, int> Values { get; set; } = new Dictionary<AttributeType, int>();
}