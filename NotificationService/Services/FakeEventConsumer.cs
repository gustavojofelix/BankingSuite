
using NotificationService.Models;
using System.Text.Json;

namespace NotificationService.Services
{
  public class FakeEventConsumer : BackgroundService
  {
    private readonly ILogger<FakeEventConsumer> _logger;

    public FakeEventConsumer(ILogger<FakeEventConsumer> logger)
    {
      _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      _logger.LogInformation(" Notification listener started");

      while(!stoppingToken.IsCancellationRequested)
      {
        // Simulate receiving an eventmevery 10 seconds
        var fakeEvent = new TransactionCompletedEvent
        {
          FromAccountId = Guid.NewGuid(),
          ToAccountId = Guid.NewGuid(),
          Amount = new Random().Next(10, 1000),
          CompletedAt = DateTime.UtcNow
        };

        var json = JsonSerializer.Serialize(fakeEvent);
        _logger.LogInformation("Event received: {Event}", json);

        // simulate sending notification
        await SendNotificationAsync(fakeEvent);

       
       await Task.Delay(10000, stoppingToken); // wait 10s
      }
    }

    private Task SendNotificationAsync(TransactionCompletedEvent evt)
    {
      _logger.LogInformation("Sending notification: ${0} transferred from {1} to {2} at {3}",
          evt.Amount, evt.FromAccountId, evt.ToAccountId, evt.CompletedAt);
      return Task.CompletedTask;
    }
  }
}
