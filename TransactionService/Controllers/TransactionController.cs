using Microsoft.AspNetCore.Mvc;
using TransactionService.Events;
using TransactionService.Models;
using TransactionService.Repositories;

namespace TransactionService.Controllers
{
  [ApiController]
  [Route("api/transactions")]
  public class TransactionController : ControllerBase
  {
    private readonly TransactionRepository _repository;
    public TransactionController(TransactionRepository repository) => _repository = repository;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _repository.GetAllAsync());

    [HttpPost("transfer")]
    public async Task<IActionResult> Transfer([FromBody] Transaction request)
    {
      // Validate basic rules (real validations will call AccountService later)
      if (request.FromAccountId == request.ToAccountId)
        return BadRequest("Cannot transfer to same account.");

      if (request.Amount <= 0)
        return BadRequest("Transfer amount must be positive.");

      // Simulate success for now
      request.Status = "Completed";
      var created = await _repository.CreateAsync(request);

      // TODO: Publish TransactionCompletedEvent (next section)
      EventPublisher.PublishTransactionCompleted(request.FromAccountId, request.ToAccountId, request.Amount);

      return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
    }
  }
}
