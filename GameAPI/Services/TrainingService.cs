using GameAPI.Data;
using GameAPI.Models;
using Shared.Characters;

namespace GameAPI.Services;

public class TrainingService
{
    private readonly AppDbContext _db;
    private readonly CharacterAttributesService _characterAttributesService;

    public TrainingService(AppDbContext db, CharacterAttributesService characterAttributesService)
    {
        _db = db;
        _characterAttributesService = characterAttributesService;
    }

    internal CharacterScheduledTraining[] GetScheduledTraining(int characterId)
    {
        var scheduled = _db.ScheduledAttributeChanges.Where(s => s.CharacterId == characterId).ToArray();
        return scheduled
            .Select(s => new CharacterScheduledTraining
            {
                AttributeChange = s.AttributeChange,
                AttributeId = s.AttributeId,
                ExecuteAt = s.ExecuteAt
            })
            .ToArray();
    }

    internal CharacterScheduledTraining StartTraining(Shared.Characters.Attribute attribute, int characterId)
    {
        var scheduledTraining = GetScheduledTraining(characterId);
        if (scheduledTraining.Length > 0)
        {
            return scheduledTraining.First();
        }

        _db.ScheduledAttributeChanges.Add(new Models.ScheduledAttributeChange
        {
            CharacterId = characterId,
            AttributeChange = 1,
            AttributeId = (int)attribute,
            ExecuteAt = DateTime.UtcNow.AddSeconds(10)
        });

        _db.SaveChanges();

        return GetScheduledTraining(characterId).First();
    }

    internal ScheduledAttributeChange[] GetScheduledTrainingsToExecute(DateTime dateTime)
    {
        var scheduled = _db.ScheduledAttributeChanges.Where(s => s.ExecuteAt <= dateTime).ToArray();
        return scheduled.ToArray();
    }

    internal void ExecuteScheduledTraining(ScheduledAttributeChange scheduled)
    {
        _characterAttributesService.ModifyAttributeValue(scheduled.CharacterId, scheduled.AttributeId, scheduled.AttributeChange);

        _db.ScheduledAttributeChanges.Remove(scheduled);

        _db.SaveChanges();
    }
}
