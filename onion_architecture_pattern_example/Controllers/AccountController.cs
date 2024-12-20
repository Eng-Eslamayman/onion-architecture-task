using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

[Route("api/accounts")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet("{mobileNumber}")]
    public async Task<IActionResult> GetAccount(string mobileNumber)
    {
        var account = await _accountService.GetAccountByMobileNumberAsync(mobileNumber);
        return account == null ? NotFound() : Ok(account);
    }
}
