using Microsoft.EntityFrameworkCore;
using TransactionService.Data;
using TransactionService.Models;

namespace TransactionService.Repositories
{
  public class TransactionRepository
  {
    private readonly TransactionDbContext _context;
    public TransactionRepository(TransactionDbContext context) => _context = context;

    public async Task<List<Transaction>> GetAllAsync() => await _context.Transactions.ToListAsync();

    public async Task<Transaction> CreateAsync(Transaction txn)
    {
      _context.Transactions.Add(txn);
      await _context.SaveChangesAsync();
      return txn;
    }
  }
}
