using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentLogic commentLogic;

    public CommentController(ICommentLogic commentLogic)
    {
        this.commentLogic = commentLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Comment>> CreateAsync(CommentCreationDto commentCreationDto)
    {
        try
        {
            Comment comment = await commentLogic.CreateAsync(commentCreationDto);
            return Created($"/comments/{comment.id}", comment);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}