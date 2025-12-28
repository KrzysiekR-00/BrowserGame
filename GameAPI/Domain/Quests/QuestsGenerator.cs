namespace GameAPI.Domain.Quests;

internal class QuestsGenerator
{
    internal Quest[] GetAvailableQuests()
    {
        List<Quest> availableQuests = [];

        Random random = new();

        for (int i = 0; i < 3; i++)
        {
            var randomLocation = LocationsCollection.Collection[
                random.Next(0, LocationsCollection.Collection.Length)
                ];

            var randomEnemy = EnemyCollection.Collection[
                random.Next(0, EnemyCollection.Collection.Length)
                ];

            var quest = new Quest()
            {
                Type = "Exploration",
                Location = randomLocation,
                Enemy = randomEnemy
            };

            availableQuests.Add(quest);
        }

        return [.. availableQuests];
    }
}
