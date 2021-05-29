using COOP.Banking.BusinessEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COOP.Banking.Services.Interfaces
{
    public interface IApproverService
    {
        public Task<List<Approver>> GetApprovers();
        //public Task<List<Approver>> GetInactiveApprovers();
        //public Task<List<Approver>> GetDeletedApprovers();
        public Task<Approver> GetApprover(int approverId);
        public Task<Approver> SaveApprover(Approver approver);
        public Task<Approver> UpdateApprover(Approver approver);

    }
}
