using COOP.Banking.BusinessEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COOP.Banking.Services.Interfaces
{
    public interface IMemberService
    {
        public Task<List<Member>> GetMembers();
        public Task<List<Member>> GetInactiveMembers();
        public Task<List<Member>> GetApprovedMembers(int approve);
        public Task<List<Member>> GetDeletedMembers();
        public Task<Member> GetMember(int memberId);
        public Task<Member> GetMemberNumber(string empNumber);
        public Task<Member> GetMemberByUserId(string userId);
        public Task<Member> SaveMember(Member member);
        public Task<Member> UpdateMember(Member member);
        public Task<bool> IsCreditCommittee(int memberId);
        //public Task<Member> DeactivateMember(int Id);
        //public Task<Member> DeleteMember(int Id);
        //public Task<Member> RestoreMember(int Id);
        //public Task<Member> ActivateMember(int Id);
    }
}
