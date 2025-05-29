namespace TransactionService.Events
{
  public class EventPublisher
  {
    public static void PublishTransactionCompleted(Guid fromId, Guid toId, decimal amount)
    {
      // In a real application, this would publish to a message broker like RabbitMQ or Kafka (will do this later)
      Console.WriteLine($"TransactionCompleted: From {fromId} to {toId}, Amount: {amount}");
    }
  }
}
