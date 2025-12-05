using GameAPI.Data;
using GameAPI.Data.Mappers;
using Shared.Characters;

namespace GameAPI.Services;

public class CharacterAttributesService
{
    private readonly AppDbContext _db;

    public CharacterAttributesService(AppDbContext db, IConfiguration config)
    {
        _db = db;
    }

    //public Attributes GetAttributes(int userId)
    //{
    //    var characterAttributes = _db.CharactersAttributes.Where(u => u.CharacterId == userId).ToArray();


    //    return _db.CharactersAttributes.FirstOrDefault(u => u.CharacterId == userId)?.AttributesCsv.ToAttributes() ?? new Attributes();
    //}

    public AttributesDTO GetAttributes(int characterId)
    {
        var characterAttributes = _db.CharactersAttributes
            .Where(u => u.CharacterId == characterId)
            .ToArray();

        return characterAttributes.ToAttributesDTO();
    }

    public void SaveAttributes(AttributesDTO dto, int characterId)
    {
        var characterAttributes = dto.ToCharacterAttributes(characterId).ToArray();

        foreach (var attribute in characterAttributes)
        {
            var existedAttribute = _db.CharactersAttributes
                .FirstOrDefault(a => a.CharacterId == attribute.CharacterId && a.AttributeId == attribute.AttributeId);

            if (existedAttribute != null)
            {
                existedAttribute.AttributeValue = attribute.AttributeValue;
            }
            else
            {
                _db.CharactersAttributes.Add(attribute);
            }
        }

        _db.SaveChanges();
    }

    public void ModifyAttributeValue(int characterId, int attributeId, int value)
    {
        var existedAttribute = _db.CharactersAttributes
                .FirstOrDefault(a => a.CharacterId == characterId && a.AttributeId == attributeId);

        if (existedAttribute != null)
        {
            existedAttribute.AttributeValue += value;
        }
        else
        {
            _db.CharactersAttributes.Add(new Models.CharacterAttribute
            {
                CharacterId = characterId,
                AttributeId = attributeId,
                AttributeValue = value
            });
        }

        _db.SaveChanges();
    }
}
