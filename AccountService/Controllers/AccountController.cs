using AccountService.Models;
using AccountService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers
{

  [ApiController]
  [Route("api/accounts")]
  public class AccountController : ControllerBase
  {
    private readonly AccountRepository _repository;

    public AccountController(AccountRepository repository)
    {
      _repository = repository;
    }

    // GET: api/account
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      var accounts = await _repository.GetAllAsync();
      return Ok(accounts);
    }

    // GET: api/account/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
      var account = await _repository.GetByIdAsync(id);
      if (account == null)
        return NotFound();

      return Ok(account);
    }

    // GET: api/account/by-number/{accountNumber}
    [HttpGet("by-number/{accountNumber}")]
    public async Task<IActionResult> GetByAccountNumber(string accountNumber)
    {
      var account = await _repository.GetByAccountNumberAsync(accountNumber);
      if (account == null)
        return NotFound();

      return Ok(account);
    }

    // POST: api/account
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Account account)
    {
      var created = await _repository.CreateAsync(account);
      return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: api/account/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Account updatedAccount)
    {
      if (id != updatedAccount.Id)
        return BadRequest("ID mismatch");

      var result = await _repository.UpdateAsync(updatedAccount);
      if (result == null)
        return NotFound();

      return Ok(result);
    }

    // DELETE: api/account/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
      var success = await _repository.DeleteAsync(id);
      if (!success)
        return NotFound();

      return NoContent();
    }
  }
}
