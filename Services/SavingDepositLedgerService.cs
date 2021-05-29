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
    class SavingDepositLedgerService : ISavingDepositLedgerService
    {
        private readonly CoopBankingDataContext _context;
        public SavingDepositLedgerService(CoopBankingDataContext context)
        {
            _context = context;
        }

        public async Task<List<SavingDepositLedger>> GetLedgerByMemId(int memberId)
        {
            var savingDepositLedger = await _context.SavingDepositLedgers
                 .Include(s => s.Member)
                 .Include(s => s.TransactionType)
                 .Where(s => s.MemberId == memberId)
                 .ToListAsync();
            return savingDepositLedger;
        }

        public async Task<List<SavingDepositLedger>> GetLedgerByMemIdTrxAndSavingsType(int memberId, int trxId, int savingsType)
        {
            var savingDepositLedger = await _context.SavingDepositLedgers
                 .Include(s => s.Member)
                 .Include(s => s.TransactionType)
                 .Where(s => s.MemberId == memberId
                     && s.TransactionType.Id == trxId
                     && s.SavingsType == savingsType)
                 .ToListAsync();
            return savingDepositLedger;
        }

        public async Task<SavingDepositLedger> GetMemberPreviousSavingLedger(int memberId, int savingsType)
        {
            var savingDepositLedger = await _context.SavingDepositLedgers
                .Where(s => s.MemberId == memberId && s.SavingsType == savingsType)
                .Include(s => s.Member)
                .Include(s => s.TransactionType)
                .LastOrDefaultAsync();
            return savingDepositLedger;
        }

        public async Task<SavingDepositLedger> GetSavingDepositLedger(int savingDepositLedgerId)
        {
            var savingDepositLedger = await _context.SavingDepositLedgers
                .Where(s => s.Id == savingDepositLedgerId)
                .Include(s => s.Member)
                .Include(s => s.TransactionType)
                .FirstOrDefaultAsync();
            return savingDepositLedger;
        }

        public async Task<List<SavingDepositLedger>> GetSavingDepositLedgers()
        {
            var savingDepositLedgers = await _context.SavingDepositLedgers
                .Include(s => s.Member)
                .Include(s => s.TransactionType)
                .ToListAsync();
            return savingDepositLedgers;
        }

        public async Task<SavingDepositLedger> SaveSavingDepositLedger(SavingDepositLedger savingDepositLedger)
        {
            _context.SavingDepositLedgers.Add(savingDepositLedger);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return savingDepositLedger;
        }

        public async Task<SavingDepositLedger> UpdateSavingDepositLedger(SavingDepositLedger savingDepositLedger)
        {
            _context.Attach(savingDepositLedger).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return savingDepositLedger;
        }
    }
}
