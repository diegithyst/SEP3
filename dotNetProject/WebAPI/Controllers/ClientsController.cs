using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientLogic _clientLogic;

    public ClientsController(IClientLogic clientLogic)
    {
        _clientLogic = clientLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Client>> CreateAsync(ClientCreationDTO dto)
    {
        try
        {
            Client client = await _clientLogic.CreateAsync(dto);
            return Created($"/clients/{client.identityDocument}", client);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    /*[HttpGet]
    public async Task<ActionResult<IEnumerable<Client>>> GetByNameAsync([FromQuery] string? name)
    {
        try
        {
            SearchClientParametersDto parameters = new(name);
            IEnumerable<Client> clients = await _clientLogic.GetByNameAsync(parameters);
            return Ok(clients);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }*/

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Client?>>> GetAsync([FromQuery] long? id)
    {
        try
        {
            SearchClientParametersDto parameters = new(id, null);
            IEnumerable<Client> clients = await _clientLogic.GetAsync(parameters);
            return Ok(clients);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}