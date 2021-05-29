using COOP.Banking.BusinessEntities;
using COOP.Banking.Data;
using COOP.Banking.Data.DTO;
using COOP.Banking.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COOP.Banking.Services
{
    public class MemberBalanceService : IMemberBalanceService
    {
        private readonly CoopBankingDataContext _context;

        public MemberBalanceService(CoopBankingDataContext context)
        {
            _context = context;
        }

        public async Task<decimal> GetMeberSavingsBalances(int itemId, Enums.BalanceType savngs, int MemberId)
        {
            decimal balance = 0;
            var MemberBalance = await _context.MemberBalances.FirstOrDefaultAsync(x => x.BalanceType == savngs && x.ItemId == itemId && x.MemberId == MemberId);
            if (MemberBalance != null)
            {
                balance = MemberBalance.CurrentBalance;
            }
            return balance;
        }
        public async void UpdateMemberBalance(MemberBalance memberBalance)
        {
            _context.Attach(memberBalance).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task<MemberBalance> GetMemberBalance(int memberBalanceId)
        {
            return await _context.MemberBalances
                .Where(x => x.Id == memberBalanceId)
                .FirstOrDefaultAsync();
        }
        public async Task<MemberBalance> GetMemberBalanceByItemId(int itemId)
        {
            return await _context.MemberBalances
                .Where(x => x.ItemId == itemId)
                .FirstOrDefaultAsync();
        }

        public async Task<List<MemberBalance>> GetMemberBalances()
        {
            return await _context.MemberBalances.ToListAsync();
        }

        public async Task<BalancesDTO> GetAllMemberBalances(int MemberId)
        {
            BalancesDTO Balance = new BalancesDTO();
            Balance.MemberId = MemberId;
            var MemberBalances = await _context.MemberBalances.Where(x => x.MemberId == MemberId).ToListAsync();
            if (MemberBalances != null && MemberBalances.Count > 0)
            {
                decimal totalLoansBalances = 0;
                var loanBalances = MemberBalances.Where(x => x.BalanceType.Equals(Enums.BalanceType.Loans)).ToList();
                if (loanBalances != null && loanBalances.Count > 0)
                {
                    totalLoansBalances = loanBalances.Sum(x => x.CurrentBalance);

                }
                Balance.TotalLoanBalance = totalLoansBalances;
                var SavingsBalance = MemberBalances.FirstOrDefault(x => x.BalanceType.Equals(Enums.BalanceType.MemberSavings) && x.ItemId == (int)Enums.SavingsType.savings);
                if (SavingsBalance != null)
                {
                    Balance.SavingsBalance = SavingsBalance.CurrentBalance;
                }
                var DepositBalance = MemberBalances.FirstOrDefault(x => x.BalanceType.Equals(Enums.BalanceType.MemberSavings) && x.ItemId == (int)Enums.SavingsType.deposit);
                if (DepositBalance != null)
                {
                    Balance.SpecialDepositBalance = DepositBalance.CurrentBalance;
                }


            }
            var MemberSavings = await _context.MemberSavings.Where(x => x.MemberId == MemberId).ToListAsync();
            if (MemberSavings != null && MemberSavings.Count > 0)
            {
                var MonthlySavings = MemberSavings.FirstOrDefault(x => x.Type == (int)Enums.SavingsType.savings);
                var MonthlyDepositSavings = MemberSavings.FirstOrDefault(x => x.Type == (int)Enums.SavingsType.deposit);

                if (MonthlySavings != null)
                {
                    Balance.MonthlyDeductions = MonthlySavings.SavingsAmount;
                }
                if (MonthlyDepositSavings != null)
                {
                    Balance.MonthlyDepositDeductions = MonthlySavings.SavingsAmount;
                }
            }
            return Balance;
        }
    }
}
