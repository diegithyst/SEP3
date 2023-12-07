using Domain.DTOs;
using Domain.Model;

namespace Application.DaoInterfaces;

public interface IGrpcAccountServices
{
    Task<Account> Create(Account account);
    Task<IEnumerable<Account>> GetByOwnerId(long ownerId);
    Task<Account> GetById(long id);
    Task UpdateAccount(AccountUpdateDTO accountUpdateDTO);
    Task<Boolean> Delete(long id);
}