using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class CommentLogic : ICommentLogic
{
    private readonly IPostDao _postDao;
    private readonly IUserDao _userDao;
    private readonly ICommentDao _commentDao;

    public CommentLogic(IPostDao postDao, IUserDao userDao, ICommentDao commentDao)
    {
        _postDao = postDao;
        _userDao = userDao;
        _commentDao = commentDao;
    }


    public async Task<Comment> CreateAsync(CommentCreationDto comment)
    {
        Comment? existing = await _commentDao.GetByContentAsync(comment.text);
        
        User? existing2 = await _userDao.GetByIdAsync(comment.authorId);

        Post? existing3 = await _postDao.GetByIdAsync(comment.postId);


        if (comment.text.Equals(existing?.text) && existing2?.id == comment.authorId && existing3.id == comment.postId)
        {
            throw new Exception("This comment already exists!");
        }

        ValidateData(comment);
        Comment toCreate = new Comment(comment.text, existing2, existing3);
        Comment created = await _commentDao.CreateAsync(toCreate);
        return created;
    }
    
    

    private static void ValidateData(CommentCreationDto commentCreationDto)
    {
        string text = commentCreationDto.text;
        if (text.Length > 200)
        {
            throw new Exception("Comments must now exceed 200 characters");
        }
    }
}