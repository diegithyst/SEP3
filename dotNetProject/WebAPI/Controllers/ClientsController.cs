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
    public async Task<ActionResult<IEnumerable<Client?>>> GetAsync()
    {
        try
        {
            ClientBasicDTO parameters = new ClientBasicDTO{ username = null, password = null};
            IEnumerable<Client> clients = await _clientLogic.GetAll();
            return Ok(clients);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Client>> GetByIdAsync([FromRoute] long id)
    {
        try
        {
            Client client = await _clientLogic.GetByIdAsync(id);
            return Ok(client);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateAsync([FromBody] ClientUpdateDTO clientUpdate)
    {
        try
        {
            await _clientLogic.UpdateAsync(clientUpdate);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

}