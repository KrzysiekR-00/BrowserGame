namespace GameAPI.Domain.Quests;

internal class QuestsGenerator
{
    internal string[] GetAvailableQuests()
    {
        List<string> availableQuests = new List<string>();

        Random random = new();

        for (int i = 0; i < 3; i++)
        {
            var randomLocation = LocationsCollection.Collection[
                random.Next(0, LocationsCollection.Collection.Length)
                ];
            var randomEnemy = EnemyCollection.Collection[
                random.Next(0, EnemyCollection.Collection.Length)
                ];

            availableQuests.Add($"Exploration - {randomLocation} - {randomEnemy}");
        }

        return availableQuests.ToArray();
    }
}
