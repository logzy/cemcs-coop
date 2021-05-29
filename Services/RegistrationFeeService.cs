using COOP.Banking.BusinessEntities;
using COOP.Banking.Data;
using COOP.Banking.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace COOP.Banking.Services
{
    public class RegistrationFeeService : IRegistrationFeeService
    {
        private readonly CoopBankingDataContext _context;

        public RegistrationFeeService(CoopBankingDataContext context)
        {
            _context = context;

        }
        public async Task<RegistrationFee> GetRegFeeByMemId(int memId)
        {
            return await _context.RegistrationFees.Where(x => x.MemberTypeId == memId).FirstOrDefaultAsync();
        }

        public async Task<RegistrationFee> SaveRegFee(RegistrationFee RegFee)
        {
            _context.RegistrationFees.Add(RegFee);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RegFee;
        }

        public async Task<RegistrationFee> UpdateRegFee(RegistrationFee RegFee)
        {
            _context.Attach(RegFee).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RegFee;
        }
    }
}