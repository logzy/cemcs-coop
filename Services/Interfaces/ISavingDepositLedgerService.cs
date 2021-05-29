using COOP.Banking.BusinessEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COOP.Banking.Services.Interfaces
{
    public interface ISavingDepositLedgerService
    {
        public Task<List<SavingDepositLedger>> GetSavingDepositLedgers();
        public Task<SavingDepositLedger> GetSavingDepositLedger(int savingDepositLedgerId);
        public Task<SavingDepositLedger> SaveSavingDepositLedger(SavingDepositLedger savingDepositLedger);
        public Task<SavingDepositLedger> UpdateSavingDepositLedger(SavingDepositLedger savingDepositLedger);
        public Task<SavingDepositLedger> GetMemberPreviousSavingLedger(int memberId, int savingsType);

        public Task<List<SavingDepositLedger>> GetLedgerByMemIdTrxAndSavingsType(int memberId, int trxId, int savingsType);
        public Task<List<SavingDepositLedger>> GetLedgerByMemId(int memberId);
    }
}
