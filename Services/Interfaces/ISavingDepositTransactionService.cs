using COOP.Banking.BusinessEntities;
using COOP.Banking.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COOP.Banking.Services.Interfaces
{
    public interface ISavingDepositTransactionService
    {
        public Task<List<SavingDepositTransaction>> GetSavingDepositTransactions();
        public Task<SavingDepositTransaction> GetSavingDepositTransaction(int savingDepositTransactionId);
        public Task<SavingDepositTransaction> GetSavingDepositTransactionByTag(string tag);
        public Task<List<TransactionType>> GetTransactionTypes();
        public Task<SavingDepositTransaction> SaveSavingDepositTransaction(SavingDepositTransaction savingDepositTransaction);
        public Task<SavingDepositTransaction> UpdateSavingDepositTransaction(SavingDepositTransaction savingDepositTransaction);
        public Task<WithdrawalApplication> SaveWithdrawalApplication(WithdrawalApplication application, string UserId);
        public Task<ServiceResponse<WithdrawalApplication>> SaveWithdrawal(WithdrawalApplication application, string UserId);
        public Task<List<SavingDepositTransaction>> GetUnreflectedTransactions();
        public Task<ServiceResponse<TransferApplication>> SaveTransfer(TransferApplication application, string UserId);
    }
}
