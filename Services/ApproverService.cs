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
    class ApproverService : IApproverService
    {
        private readonly CoopBankingDataContext _context;
        public ApproverService(CoopBankingDataContext context)
        {
            _context = context;
        }
        public async Task<Approver> GetApprover(int approverId)
        {
            return await _context.Approvers.Where(x => x.Id == approverId).FirstOrDefaultAsync();
        }

        public async Task<List<Approver>> GetApprovers()
        {
            return await _context.Approvers.ToListAsync();
        }

        public async Task<Approver> SaveApprover(Approver approver)
        {
            _context.Approvers.Add(approver);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return approver;
        }

        public async Task<Approver> UpdateApprover(Approver approver)
        {
            _context.Attach(approver).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return approver;
        }
    }
}
