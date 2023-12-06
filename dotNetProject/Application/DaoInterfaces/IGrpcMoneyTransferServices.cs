using Domain.DTOs;
using Domain.Model;

namespace Application.DaoInterfaces;

public interface IGrpcMoneyTransferServices
{
    Task<MoneyTransfer> TransferMoney(MoneyTransferCreationDto dto);
    Task<MoneyTransfer?> GetMoneyTransferById(long id);
    Task<IEnumerable<MoneyTransfer>> GetMoneyTransfers(long accountId);

}