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
    class ModuleApproverService : IModuleApproverService
    {
        private readonly CoopBankingDataContext _context;
        public ModuleApproverService(CoopBankingDataContext context)
        {
            _context = context;
        }
        public async Task<ModuleApprover> GetModuleApprover(int moduleApproverId)
        {
            return await _context.ModuleApprovers.Where(x => x.Id == moduleApproverId)
                .Include(m => m.Module)
                .Include(m => m.ModuleApproverNameStores)
                .FirstOrDefaultAsync();
        }
        public async Task<ModuleApprover> GetModuleApproverByModuleId(int moduleId)
        {
            return await _context.ModuleApprovers.Where(x => x.ModuleId == moduleId)
                .Include(m => m.Module)
                .Include(m => m.ModuleApproverNameStores)
                .FirstOrDefaultAsync();
        }
        public async Task<Module> GetModuleByNormalizedName(string name)
        {
            return await _context.Modules.Where(x => x.NormalizedName == name)
                .FirstOrDefaultAsync();
        }

        public async Task<List<ModuleApprover>> GetModuleApprovers()
        {
            return await _context.ModuleApprovers
                .Include(m => m.Module)
                .Include(m => m.ModuleApproverNameStores)
                .ToListAsync();
        }

        public async Task<ModuleApprover> SaveModuleApprover(ModuleApprover moduleApprover)
        {
            _context.ModuleApprovers.Add(moduleApprover);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return moduleApprover;
        }

        public async Task<ModuleApprover> UpdateModuleApprover(ModuleApprover moduleApprover)
        {
            _context.Attach(moduleApprover).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return moduleApprover;
        }

        public async void RemoveApproverNameStore(ModuleApproverNameStore moduleApproverNameStore)
        {
            _context.Remove(moduleApproverNameStore).State = EntityState.Deleted;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Module>> GetModules()
        {
            return await _context.Modules.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<List<Module>> GetModulesWithoutApprovers()
        {
            var currentMAs = await GetModuleApprovers();
            return await _context.Modules
                .Where(x => !currentMAs.Any(t => t.ModuleId == x.Id))
               .ToListAsync();
        }
    }
}
