using COOP.Banking.BusinessEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COOP.Banking.Services.Interfaces
{
    public interface IMemberSavingService
    {
        public Task<List<MemberSaving>> GetAllMemSavings();
        public Task<MemberSaving> GetSavingsById(int id, int savingsType);
        public Task<List<MemberSaving>> GetSavingsByMemId(int MemId);
        public Task<List<MemberSaving>> GetSavingsByMemberIdAndType(int MemberId, int savingsType);
        public Task<MemberSaving> SaveMemSaving(MemberSaving MemSavings);
        public void CreateNewMemberSavingAndDeposit(decimal savingsAmount, int memberId);
        public Task<MemberSaving> UpdateMemSaving(MemberSaving MemSavings);
    }
}