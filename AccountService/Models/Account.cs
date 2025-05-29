namespace AccountService.Models
{
  public class Account
  {
    public Guid Id { get; set; }
    public string CustomerName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  }
}
