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
    public class BeneficiaryService : IBeneficiaryService
    {

        private readonly CoopBankingDataContext _context;
        public BeneficiaryService(CoopBankingDataContext context)
        {
            _context = context;
        }

        public async Task<LoanBeneficiary> AddLoanBeneficary(int BeneficiaryId, int LoanApplicationId)
        {
            LoanBeneficiary lb = new LoanBeneficiary();
            var existingAccount = await _context.LoanBeneficiaries.FirstOrDefaultAsync(x => x.LoanApplicationId == LoanApplicationId);
            if (existingAccount != null)
            {
                _context.LoanBeneficiaries.Add(lb);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return lb;
        }

        public async Task<List<Beneficiary>> GetBeneficiariesByMemberId(int MemeberId)
        {
            var beneficiaries = await _context.Beneficiaries.Where(x => x.MemberId == MemeberId).Include(a => a.Bank).ToListAsync();
            return beneficiaries;
        }

        public async Task<Beneficiary> SaveBeneficiary(Beneficiary beneficiary)
        {
            var existingAccount = await _context.Beneficiaries.FirstOrDefaultAsync(x => x.AccountName == beneficiary.AccountName && x.BankId == beneficiary.BankId && x.MemberId == beneficiary.MemberId);
            if (existingAccount != null)
            {
                _context.Beneficiaries.Add(beneficiary);
                //try
                //{
                //    await _context.SaveChangesAsync();
                //}
                //catch (Exception ex)
                //{
                //    throw ex;
                //}
                return beneficiary;
            }
            else
            {
                return existingAccount;
            }


        }
    }
}
