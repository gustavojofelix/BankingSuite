//using Banking.Contracts.Events;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NotificationService.Services
{
    public class RabbitMqEventConsumer : BackgroundService
    {
        private readonly ILogger<RabbitMqEventConsumer> _logger;
        private IConnection _connection;
        private IModel _channel;

        public RabbitMqEventConsumer(ILogger<RabbitMqEventConsumer> logger)
        {
            _logger = logger;
            var factory = new ConnectionFactory()
            {
                HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "rabbitmq",
                Port = int.TryParse(
                    Environment.GetEnvironmentVariable("RABBITMQ_PORT"),
                    out var port
                )
                    ? port
                    : 5672,
                UserName = "guest",
                Password = "guest",
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(
                queue: "transactions",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );
        }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
      var consumer = new EventingBasicConsumer(_channel);

      consumer.Received += (model, ea) =>
      {
        var body = ea.Body.ToArray();
        var json = Encoding.UTF8.GetString(body);
        var evt = JsonSerializer.Deserialize<Banking.Contracts.Events.TransactionCompleted>(json);

        if (evt != null) // Ensure evt is not null before dereferencing
        {
          _logger.LogInformation("Received transaction event: {json}", json);

          // Simulate sending notification
          _logger.LogInformation(
                "Sending notification for transaction from {FromAccountId} to {ToAccountId} of amount {Amount} at {CompletedAt}",
                evt.FromAccountId,
                evt.ToAccountId,
                evt.Amount,
                evt.CompletedAt
            );
        }
        else
        {
          _logger.LogWarning("Failed to deserialize transaction event: {json}", json);
        }
      };

      _channel.BasicConsume(queue: "transactions", autoAck: true, consumer: consumer);

      return Task.CompletedTask;
    }
    }
}
