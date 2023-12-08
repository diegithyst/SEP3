using Application.Logic;
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
    public async Task<ActionResult<Account>> CreateAsync(AccountCreationDTO dto)
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
    public async Task<ActionResult<IEnumerable<Account?>?>> GetByClientIdAsync([FromQuery] long ownerId)
    
    {
        try
        {
            IEnumerable<Account?> accounts = await _accountLogic.GetByClientIdAsync(ownerId);
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

    [HttpDelete("{id:long}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] long id)
    {
        try
        {
            await _accountLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateAsync([FromBody] AccountExchangeDTO dto)
    {
        try
        {
            await _accountLogic.Exchange(dto.id, dto.amount, dto.currencyFrom, dto.currencyTo);
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

}