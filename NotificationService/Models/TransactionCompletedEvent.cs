namespace NotificationService.Models
{
  public class TransactionCompletedEvent
  {
    public Guid FromAccountId { get; set; }
    public Guid ToAccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime CompletedAt { get; set; }
  }
}
