using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileData.PersistentServerServices
{
    public class GrpcMoneyTransferServices : IGrpcMoneyTransferServices
    {
        private readonly PersistentServerClient.PersistentServer.PersistentServerClient psc;

        public GrpcMoneyTransferServices(GrpcContext grpcContext)
        {
            psc = grpcContext.Psc;
        }

        public Task<MoneyTransfer> TransferMoney(MoneyTransferCreationDto dto)
        {
            PersistentServerClient.GrpcMoneyTransfer gmt = psc.MakeMoneyTransfer(new PersistentServerClient.CreateMoneyTransferDTO { SenderId = dto.SenderAccountNumber, RecipientId = dto.ReceiverAccountNumber, SenderCurrency = dto.SenderCurrency, Amount = dto.Amount });
            MoneyTransfer created = new MoneyTransfer { id = gmt.MoneyTransferId, accountNumberSender = gmt.SenderId, accountNumberRecipient = gmt.RecipientId, currency = gmt.SenderCurrency, amount = gmt.Amount };
            return Task.FromResult(created);
        }
        public Task<MoneyTransfer?> GetMoneyTransferById(long id)
        {
            PersistentServerClient.GrpcMoneyTransfer gmt = psc.GetMoneyTransferById(new PersistentServerClient.MoneyTransferBasicDTO { MoneyTransferId = id });
            if (gmt == null)
            {
                return null;
            }
            else
            {
                return Task.FromResult(new MoneyTransfer { id = gmt.MoneyTransferId, accountNumberSender = gmt.SenderId, accountNumberRecipient = gmt.RecipientId, amount = gmt.Amount, currency = gmt.SenderCurrency });

            }
        }
        public Task<IEnumerable<MoneyTransfer>> GetMoneyTransfers(long accountId)
        {
            List<MoneyTransfer> moneyTransfers = new List<MoneyTransfer>();
            PersistentServerClient.GrpcMoneyTransfers call = psc.GetMoneyTransfers(new PersistentServerClient.AccountBasicDTO { AccountId = accountId });
            foreach (PersistentServerClient.GrpcMoneyTransfer grpcMoneyTransfer in call.MoneyTransfers)
            {
                moneyTransfers.Add(new MoneyTransfer { id = grpcMoneyTransfer.MoneyTransferId, accountNumberSender = grpcMoneyTransfer.SenderId, accountNumberRecipient = grpcMoneyTransfer.RecipientId, currency = grpcMoneyTransfer.SenderCurrency, amount = grpcMoneyTransfer.Amount });
            }

            return Task.FromResult(moneyTransfers.AsEnumerable());
        }
    }
}
