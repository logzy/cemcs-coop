using COOP.Banking.BusinessEntities;
using COOP.Banking.Data;
using COOP.Banking.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COOP.Banking.Services
{
    class MemberService : IMemberService
    {
        private readonly CoopBankingDataContext _context;
        public MemberService(CoopBankingDataContext context)
        {
            _context = context;
        }

        public async Task<Member> GetMember(int memberId)
        {
            var member = await _context.Members
            .Where(m => m.Id == memberId)
            .Include(m => m.Person)
            .FirstOrDefaultAsync();
            return member;
        }

        public async Task<List<Member>> GetMembers()
        {
            return await _context.Members
            .Where(m => m.Active && !m.Deleted)
            .Include(m => m.Person).ToListAsync();

        }


        public async Task<List<Member>> GetInactiveMembers()
        {
            return await _context.Members
                .Where(m => !m.Active)
                .Include(m => m.Person).ToListAsync();
        }
        public async Task<List<Member>> GetDeletedMembers()
        {
            return await _context.Members
                .Where(m => m.Deleted)
                .Include(m => m.Person).ToListAsync();
        }

        public async Task<Member> SaveMember(Member member)
        {
            _context.Members.Add(member);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return member;
        }
        public async Task<Member> UpdateMember(Member member)
        {
            _context.Attach(member).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return member;
        }

        public async Task<Member> GetMemberByUserId(string userId)
        {
            var member = await _context.Members
            // .Where(m => m.UserId == userId)
            .Include(m => m.Person)
            .FirstOrDefaultAsync(m => m.UserId == userId);
            return member;
        }

        public async Task<bool> IsCreditCommittee(int memberId)
        {
            return await _context.CreditCommittees.AnyAsync(e => e.MemberId == memberId);
        }

        public async Task<List<Member>> GetApprovedMembers(int approve)
        {
            bool result = Convert.ToBoolean(approve);
            return await _context.Members
            .Where(m => m.Approved == result && !m.Deleted)
            .Include(m => m.Person).ToListAsync();
        }

        public async Task<Member> GetMemberNumber(string empNumber)
        {
            var member = await _context.Members
            .Include(m => m.Person)
            .FirstOrDefaultAsync(m => m.EmployeeNumber == empNumber);
            return member;
        }

        //public Task<Member> DeactivateMember(int memberId)
        //{
        //    throw new NotImplementedException();
        //}
        //public Task<Member> ActivateMember(int memberId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Member> DeleteMember(int memberId)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Member> RestoreMember(int memberId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
