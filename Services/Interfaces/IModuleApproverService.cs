using COOP.Banking.BusinessEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COOP.Banking.Services.Interfaces
{
    public interface IModuleApproverService
    {
        public Task<List<ModuleApprover>> GetModuleApprovers();
        public Task<ModuleApprover> GetModuleApproverByModuleId(int moduleId);
        public Task<Module> GetModuleByNormalizedName(string name);
        public Task<ModuleApprover> GetModuleApprover(int moduleApproverId);
        public Task<List<Module>> GetModules();
        public Task<List<Module>> GetModulesWithoutApprovers();
        public Task<ModuleApprover> SaveModuleApprover(ModuleApprover moduleApprover);
        public Task<ModuleApprover> UpdateModuleApprover(ModuleApprover moduleApprover);
        public void RemoveApproverNameStore(ModuleApproverNameStore moduleApproverNameStore);
    }
}
