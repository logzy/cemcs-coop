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
    public class MemberSavingService : IMemberSavingService
    {
        private readonly CoopBankingDataContext _context;

        public MemberSavingService(CoopBankingDataContext context)
        {
            _context = context;
        }
        public async Task<List<MemberSaving>> GetAllMemSavings()
        {
            return await _context.MemberSavings.Include(m => m.Member).ToListAsync();
        }
        public async Task<MemberSaving> GetSavingsById(int id, int savingsType)
        {
            return await _context.MemberSavings
            .Include(m => m.Member)
            .Where(x => x.Id == id && x.Type == savingsType)
            .FirstOrDefaultAsync();
        }
        public async Task<List<MemberSaving>> GetSavingsByMemId(int MemId)
        {
            return await _context.MemberSavings
            .Include(m => m.Member).Where(x => x.MemberId == MemId)
            .ToListAsync();
        }
        public async Task<List<MemberSaving>> GetSavingsByMemberIdAndType(int MemberId, int savingsType)
        {
            return await _context.MemberSavings
            .Include(m => m.Member)
            .Where(x => x.MemberId == MemberId && x.Type == savingsType)
            .ToListAsync();
        }

        public async Task<MemberSaving> SaveMemSaving(MemberSaving MemSavings)
        {
            _context.MemberSavings.Add(MemSavings);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return MemSavings;
        }

        public async void CreateNewMemberSavingAndDeposit(decimal savingsAmount, int memberId)
        {
            // For Savings
            _context.MemberSavings.Add(new MemberSaving
            {
                MemberId = memberId,
                SavingsAmount = savingsAmount,
                Type = (int)Enums.SavingsType.savings
            });

            //For Deposit
            _context.MemberSavings.Add(new MemberSaving
            {
                MemberId = memberId,
                SavingsAmount = 0,
                Type = (int)Enums.SavingsType.deposit
            });
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<MemberSaving> UpdateMemSaving(MemberSaving MemSavings)
        {
            _context.Attach(MemSavings).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return MemSavings;
        }
    }
}