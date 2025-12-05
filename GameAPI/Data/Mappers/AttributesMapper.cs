using GameAPI.Models;
using Shared.Characters;

namespace GameAPI.Data.Mappers;

internal static class AttributesMapper
{
    internal static AttributesDTO ToAttributesDTO(this CharacterAttribute[] characterAttributes)
    {
        var dto = new AttributesDTO();

        var attributes = Enum.GetValues<Shared.Characters.Attribute>();
        foreach (var attribute in attributes)
        {
            var value = characterAttributes
                .FirstOrDefault(c => c.AttributeId == (int)attribute)?
                .AttributeValue ?? 0;

            dto.Values.Add(attribute, value);
        }

        return dto;
    }

    internal static IEnumerable<CharacterAttribute> ToCharacterAttributes(this AttributesDTO dto, int characterId)
    {
        foreach (var attribute in dto.Values)
        {
            yield return new CharacterAttribute
            {
                CharacterId = characterId,
                AttributeId = (int)attribute.Key,
                AttributeValue = attribute.Value
            };
        }
    }
}
