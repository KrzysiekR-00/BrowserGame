using GameAPI.Data;
using GameAPI.Models;
using Shared.Characters;

namespace GameAPI.Services;

public class CharacterService
{
    private readonly AppDbContext _db;
    private readonly CharacterAttributesService _attributesService;

    public CharacterService(AppDbContext db, IConfiguration config, CharacterAttributesService attributesService)
    {
        _db = db;
        _attributesService = attributesService;
    }

    public void AddCharacter(int userId)
    {
        var character = new Character
        {
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        _db.Characters.Add(character);

        _db.SaveChanges();

        //_db.CharactersAttributes.Add(new CharacterAttributes
        //{
        //    CharacterId = character.Id,
        //    AttributesCsv = new Shared.Characters.Attributes(1, 2, 3).ToCsv()
        //});

        //_db.SaveChanges();

        var dto = new AttributesDTO();
        _attributesService.SaveAttributes(dto, character.Id);
    }

    public Character GetByUserId(int userId)
    {
        return _db.Characters.FirstOrDefault(c => c.UserId == userId);
    }
}
