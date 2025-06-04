using AccountService.Models;

namespace AccountService.Data
{
  public class DbInitializer
  {
    public static void SeedAccounts(AccountDbContext context)
    {
      if (context.Accounts.Any()) return;

      var accounts = new List<Account>
        {
            new Account { Id = Guid.NewGuid(), CustomerName = "John Doe", AccountNumber = "10000001", Balance = 1000.00m, Email = "johndoe@domain.com" },
            new Account { Id = Guid.NewGuid(), CustomerName = "Jane Smith", AccountNumber = "10000002", Balance = 2500.00m, Email = "johndoe@domain.com" },
            new Account { Id = Guid.NewGuid(), CustomerName = "Alice Johnson", AccountNumber = "10000003", Balance = 500.00m, Email = "johndoe@domain.com" },
            new Account { Id = Guid.NewGuid(), CustomerName = "Bob Lee", AccountNumber = "10000004", Balance = 1500.00m, Email = "johndoe@domain.com" },
            new Account { Id = Guid.NewGuid(), CustomerName = "Eve Watson", AccountNumber = "10000005", Balance = 3000.00m, Email = "johndoe@domain.com" },
        };

      context.Accounts.AddRange(accounts);
      context.SaveChanges();
    }
  }
}
