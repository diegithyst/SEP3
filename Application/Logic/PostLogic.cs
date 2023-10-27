using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao _postDao;
    private readonly IUserDao _userDao;

    public PostLogic(IPostDao postDao, IUserDao userDao)
    {
        _postDao = postDao;
        _userDao = userDao;
    }

    public async Task<Post> CreateAsync(PostCreationDTO postToCreate)
    {
        Post? existing = await _postDao.GetByTitle(postToCreate.title);
        Post? existing2 = await _postDao.GetByBody(postToCreate.body);

        User? user = await _userDao.GetByIdAsync(postToCreate.ownerID);

        if (postToCreate.title.Equals(existing?.title) && postToCreate.body.Equals(existing2?.body) && postToCreate.ownerID.Equals(user?.id))
        {
            throw new Exception("This post already exists");
        }
        
        
        ValidateData(postToCreate);
        Post toCreate = new Post(user, postToCreate.title, postToCreate.body);
        Post created = await _postDao.CreateAsync(toCreate);

        return created;
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchPostParametersDto)
    {
        return _postDao.GetAsync(searchPostParametersDto);
    }

    public async Task UpdateAsync(PostEditDto post)
    {
        Post? toEdit = await _postDao.GetByIdAsync(post.postId);

        if (toEdit == null)
        {
            throw new Exception($"The post with id: {post.postId} doesn't exist");
        }

        User? user = await _userDao.GetByIdAsync(post.ownerId);

        if (user == null)
        {
            throw new Exception($"User with id: {post.ownerId} doesn't exist");
        }

        Post edited = new Post(user, post.newTitle, post.newBody)
        {
            edited = true,
            id = toEdit.id
        };
        
        validatePost(edited);

        await _postDao.UpdateAsync(edited);
    }

    public async Task<PostBasicDto> GetByIdAsync(int id)
    {
        Post? post = await _postDao.GetByIdAsync(id);
        if (post == null)
        {
            throw new Exception($"Post with id {id} not found");
        }

        return new PostBasicDto(post.author, post.title, post.body, post.upVote, post.downVote, post.edited);
    }

    private static void validatePost(Post post)
    {
        string title = post.title;
        string body = post.body;
        if (title.Length < 3)
        {
            throw new Exception("Title must be at least 3 characters long!");
        }

        if (title.Length > 100)
        {
            throw new Exception("Title must be less that 100 characters long");
        }

        if (body.Length > 1000)
        {
            throw new Exception("Body must be less than 1000 characters long");
        }
    }

    private static void ValidateData(PostCreationDTO postToCreate)
    {
        string title = postToCreate.title;
        string body = postToCreate.body;
        if (title.Length < 3)
        {
            throw new Exception("Title must be at least 3 characters long!");
        }

        if (title.Length > 100)
        {
            throw new Exception("Title must be less that 100 characters long");
        }

        if (body.Length > 1000)
        {
            throw new Exception("Body must be less than 1000 characters long");
        }
    }
}