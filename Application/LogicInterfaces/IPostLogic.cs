using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IPostLogic
{
    Task<Post> CreateAsync(PostCreationDTO postToCreate);
    Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchPostParametersDto);

    Task UpdateAsync(PostEditDto post);
    Task<PostBasicDto> GetByIdAsync(int id);

}