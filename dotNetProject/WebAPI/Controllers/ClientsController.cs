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
            return Created($"/clients/{client.id}", client);
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

    [HttpGet("{id:long}")]
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

    [HttpPatch ("{id:long}")]
    public async Task<ActionResult> UpdateAsync([FromRoute] long id, [FromBody] ClientUpdateDTO dto)
    {
        try
        {
            await _clientLogic.UpdateAsync(dto, id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

}