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
    public AccountController(AccountRepository repository) => _repository = repository;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _repository.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
      var account = await _repository.GetByIdAsync(id);
      return account is null ? NotFound() : Ok(account);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Account account)
    {
      var created = await _repository.CreateAsync(account);
      return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
  }
}
