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

    [HttpGet("GetByUsername/")]
    public async Task<ActionResult<IEnumerable<Client>>> GetByUsernameAsync([FromQuery] string? username)
    {
        try
        {
            Client client = await _clientLogic.GetByUsernameAsync(username);
            return Ok(client);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientUpdateDTO?>>> GetAsync()
    {
        try
        {
            ClientBasicDTO parameters = new ClientBasicDTO{ username = null, password = null};
            IEnumerable<Client?> clients = await _clientLogic.GetAll();
            IList<ClientUpdateDTO?>? clientsSend = null;
            foreach (var client in clients)
            {
                clientsSend.Add(new ClientUpdateDTO(client.id)
                {
                    birthday = client.birthday,
                    country = client.country,
                    firstname = client.firstname,
                    identityDocument = client.identityDocument,
                    lastname = client.lastname,
                    password = client.password,
                    planType = client.planType.getName()
                });
            }

            IEnumerable<ClientUpdateDTO?>? clients2 = clientsSend?.AsEnumerable();
            return Ok(clients2);
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
            Client? client = await _clientLogic.GetByIdAsync(id);

            ClientUpdateDTO test = new ClientUpdateDTO(id)
            {
                birthday = client.birthday,
                country = client.country,
                firstname = client.firstname,
                identityDocument = client.identityDocument,
                lastname = client.lastname,
                password = client.password,
                planType = client.planType.getName()
            };
            return Ok(test);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateAsync( [FromBody] ClientUpdateDTO dto)
    {
        try
        {
            await _clientLogic.UpdateAsync(dto);
            return Ok();
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
            await _clientLogic.DeleteClientAsync(id);
            return Ok();
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}