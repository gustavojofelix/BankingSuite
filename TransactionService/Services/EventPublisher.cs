using Banking.Contracts.Events;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace TransactionService.Services
{
  public class EventPublisher
  {
    private readonly IConfiguration _config;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public EventPublisher(IConfiguration config)
    {
      _config = config;

      var factory = new ConnectionFactory()
      {
        HostName = _config["RabbitMQ:Host"] ?? "localhost"
      };

      _connection = factory.CreateConnection();
      _channel = _connection.CreateModel();
      _channel.QueueDeclare(queue: "transactions", durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    public void Publish(TransactionCompleted evt)
    {
      var json = JsonSerializer.Serialize(evt);
      var body = Encoding.UTF8.GetBytes(json);
      _channel.BasicPublish(exchange: "", routingKey: "transactions", basicProperties: null, body: body);
    }
  }
}
