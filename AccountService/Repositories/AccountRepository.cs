using AccountService.Data;
using AccountService.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountService.Repositories
{
  public class AccountRepository
  {
    private readonly AccountDbContext _context;
    public AccountRepository(AccountDbContext context) => _context = context;

    public async Task<List<Account>> GetAllAsync() => await _context.Accounts.ToListAsync();

    public async Task<Account?> GetByIdAsync(Guid id) => await _context.Accounts.FindAsync(id);

    public async Task<Account> CreateAsync(Account account)
    {
      _context.Accounts.Add(account);
      await _context.SaveChangesAsync();
      return account;
    }
  }
}
