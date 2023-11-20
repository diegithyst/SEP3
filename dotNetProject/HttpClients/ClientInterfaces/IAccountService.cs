﻿using Domain.DTOs;
using Domain.Model;

namespace HttpClients.ClientInterfaces;

public interface IAccountService
{
    Task<Account> GetAccountAsync(long id);
    Task CreateAsync(AccountCreationDTO dto);
    Task<ICollection<Account>> GetListAsync();
    Task UpdateAsync(AccountUpdateDto dto);
}