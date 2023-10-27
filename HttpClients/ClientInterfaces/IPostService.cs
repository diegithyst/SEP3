using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{ 
    Task CreateAsync(PostCreationDTO dto);
    
    Task<ICollection<Post>> GetAsync(
        string? userName, 
        bool? editedStatus, 
        string? titleContains,
        string? bodyContains
    );

    Task<PostBasicDto> GetByIdAsync(int id);

    public Task UpdateAsync(PostEditDto dto);
}