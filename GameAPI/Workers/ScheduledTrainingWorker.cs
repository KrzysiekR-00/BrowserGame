using GameAPI.Services;

namespace GameAPI.Workers;

public class ScheduledTrainingWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public ScheduledTrainingWorker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var trainingService = scope.ServiceProvider.GetRequiredService<TrainingService>();

            var toExecute = trainingService.GetScheduledTrainingsToExecute(DateTime.UtcNow);

            foreach (var t in toExecute)
            {
                Console.WriteLine($"to execute: {t.CharacterId} {t.AttributeId} {t.ExecuteAt}");

                trainingService.ExecuteScheduledTraining(t);
            }

            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}
