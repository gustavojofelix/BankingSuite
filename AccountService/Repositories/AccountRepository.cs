using AccountService.Data;
using AccountService.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Repositories
{
  public class AccountRepository
  {
    private readonly AccountDbContext _context;

    public AccountRepository(AccountDbContext context)
    {
      _context = context;
    }

    public async Task<List<Account>> GetAllAsync()
    {
      return await _context.Accounts.ToListAsync();
    }

    public async Task<Account?> GetByIdAsync(Guid id)
    {
      return await _context.Accounts.FindAsync(id);
    }

    public async Task<Account?> GetByAccountNumberAsync(string accountNumber)
    {
      return await _context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);
    }

    public async Task<Account> CreateAsync(Account account)
    {
      account.AccountNumber = await GenerateUniqueAccountNumberAsync();
      _context.Accounts.Add(account);
      await _context.SaveChangesAsync();
      return account;
    }

    public async Task<Account?> UpdateAsync(Account updatedAccount)
    {
      var existing = await _context.Accounts.FindAsync(updatedAccount.Id);
      if (existing == null)
        return null;

      // Update fields (excluding AccountNumber and Id)
      existing.CustomerName = updatedAccount.CustomerName;
      existing.Email = updatedAccount.Email;
      existing.Balance = updatedAccount.Balance;

      await _context.SaveChangesAsync();
      return existing;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
      var existing = await _context.Accounts.FindAsync(id);
      if (existing == null)
        return false;

      _context.Accounts.Remove(existing);
      await _context.SaveChangesAsync();
      return true;
    }

    private async Task<string> GenerateUniqueAccountNumberAsync()
    {
      string newAccountNumber;
      bool exists;

      do
      {
        newAccountNumber = GenerateAccountNumber();
        exists = await _context.Accounts.AnyAsync(a => a.AccountNumber == newAccountNumber);
      } while (exists);

      return newAccountNumber;
    }

    private string GenerateAccountNumber()
    {
      var random = new Random();
      return $"ACC-{DateTime.UtcNow.Ticks.ToString().Substring(10)}{random.Next(100, 999)}";
    }
  }
}
