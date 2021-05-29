using COOP.Banking.BusinessEntities;
using COOP.Banking.Data;
using COOP.Banking.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COOP.Banking.Services.Interfaces
{
    public interface IMemberBalanceService
    {
        public Task<List<MemberBalance>> GetMemberBalances();
        public Task<MemberBalance> GetMemberBalance(int memberBalanceId);
        public Task<MemberBalance> GetMemberBalanceByItemId(int itemId);
        public Task<decimal> GetMeberSavingsBalances(int itemId, Enums.BalanceType savngs, int MemberId);
        public Task<BalancesDTO> GetAllMemberBalances(int MemberId);
        public void UpdateMemberBalance(MemberBalance memberBalance);
    }
}
