using Application.Logic;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TransferMoneyController : ControllerBase
{
    private readonly IMoneyTransferLogic mtLogic;

    public TransferMoneyController(IMoneyTransferLogic mtLogic)
    {
        this.mtLogic = mtLogic;
    }

    [HttpPost]
    public async Task<ActionResult<MoneyTransfer>> CreateAsync(MoneyTransferCreationDto dto)
    {
        try
        {
            MoneyTransfer moneyTransfer = await mtLogic.CreateAsync(dto);
            return Ok(); //return something else
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<IEnumerable<MoneyTransfer>>> GetTransfersByAccountIdAsync([FromRoute] long id)
    {
        try
        {
            IEnumerable<MoneyTransfer> moneyTransfers = await mtLogic.GetByAccountIdAsync(id);
            return Ok(moneyTransfers);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<MoneyTransfer>> GetMoneyTransferById([FromQuery] long id)
    {
        try
        {
            MoneyTransfer? existing = await mtLogic.GetByIdAsync(id);
            return Ok(existing);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }


}