using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface IPostDao
{
    Task<Post> CreateAsync(Post post);
    Task<Post?> GetByTitle(string title);
    Task<Post?> GetByBody(string body);
    Task<Post> GetByIdAsync(int id);
    Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchPostParametersDto);
    
    Task UpdateAsync(Post post);
}