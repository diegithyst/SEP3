using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace FileData.DAOs;

public class PostFileDao : IPostDao
{
    private readonly FileContext _context;

    public PostFileDao(FileContext context)
    {
        _context = context;
    }

    public Task<Post> CreateAsync(Post post)
    {
        int id = 1;
        if (_context.Posts.Any())
        {
            id = _context.Posts.Max(t => t.id);
            id++;
        }

        post.id = id;
        _context.Posts.Add(post);
        _context.SaveChanges();
        return Task.FromResult(post);
    }

    public Task<Post?> GetByTitle(string title)
    {
        Post? existing = _context.Posts.FirstOrDefault(p => p.title.Equals(title, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(existing);
    }

    public Task<Post?> GetByBody(string body)
    {
        Post? existing = _context.Posts.FirstOrDefault(p => p.body.Equals(body, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(existing);    }

    public Task<Post> GetByIdAsync(int id)
    {
        Post? existing = _context.Posts.FirstOrDefault(p => p.id == id);
        return Task.FromResult(existing);
    }

    public Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchPostParametersDto)
    {
        IEnumerable < Post > posts = _context.Posts.AsEnumerable();
        if (!string.IsNullOrEmpty(searchPostParametersDto.username))
        {
            posts = _context.Posts.Where(p =>
                p.author.username.Equals(searchPostParametersDto.username, StringComparison.OrdinalIgnoreCase));
        }

        if (searchPostParametersDto.userId != null)
        {
            posts = posts.Where(p => p.author.id == searchPostParametersDto.userId);
        }

        if (searchPostParametersDto.edited != null)
        {
            posts = posts.Where(p => p.edited == searchPostParametersDto.edited);
        }

        if (!string.IsNullOrEmpty(searchPostParametersDto.titleContains))
        {
            posts = posts.Where(p =>
                p.title.Contains(searchPostParametersDto.titleContains, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrEmpty(searchPostParametersDto.bodyContains))
        {
            posts = posts.Where(p =>
                p.body.Contains(searchPostParametersDto.bodyContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(posts);
    }

    public Task UpdateAsync(Post post)
    {
        Post? existing = _context.Posts.FirstOrDefault(p => p.id == post.id);
        if (existing == null)
        {
            throw new Exception($"Post with id: {post.id} doesn't exist");
        }

        _context.Posts.Remove(existing);
        _context.Posts.Add(post);
        _context.SaveChanges();
        return Task.CompletedTask;
    }
}