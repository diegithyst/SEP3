using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountsController : ControllerBase
{
    private readonly IAccountLogic _accountLogic;

    public AccountsController(IAccountLogic accountLogic)
    {
        _accountLogic = accountLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Account>> CreateAsync([FromQuery] AccountCreationDTO dto)
    {
        try
        {
            Account account = await _accountLogic.CreateAsync(dto);
            return Created($"/accounts/{account.id}", account);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Account>>> GetByOwnerIdAsync([FromQuery] long ownerId)
    {
        try
        {
            IEnumerable<Account?> accounts = await _accountLogic.GetByOwnerIdAsync(ownerId);
            return Ok(accounts);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet ("getAccountById/")]
    public async Task<ActionResult<Account>> GetAccountById([FromQuery] long accountId)
    {
        try
        {
            Account? existing = await _accountLogic.GetByIdAsync(accountId);
            return Ok(existing);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

}