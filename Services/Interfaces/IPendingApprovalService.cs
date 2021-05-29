using COOP.Banking.BusinessEntities;
using COOP.Banking.Data;
using COOP.Banking.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COOP.Banking.Services.Interfaces
{
    public interface IPendingApprovalService<TModuleApprover> where TModuleApprover : ModuleApprover
    {
        public Task<PendingApproval> CreateApproval(TModuleApprover moduleApprover, int itemId);
        public Task<List<PendingApproval>> GetPendingApprovals();
        public Task<PendingApproval> GetPendingApproval(int pendingApprovalId);
        public Task<IList<PendingApproval>> GetUserPendingApproval(string username);

        public Task<List<PendingApprovalRequestDTO<Member>>> GetUserPendingMemberApprovals(string username);
        public Task<List<PendingApprovalRequestDTO<LoanApplication>>> GetUserPendingLoanApprovals(string username);
        public Task<List<PendingApprovalRequestDTO<SavingDepositTransaction>>> GetUserPendingSavingApprovals(string username);
        public Task<List<PendingApprovalRequestDTO<SavingDepositTransaction>>> GetUserPendingCashApprovals(string username);


        public Task<ServiceResponse<PendingApproval>> ApproveMembershipModule(PendingApproval pendingApproval, string approverUsername, bool? approvalValue);
        public Task<ServiceResponse<PendingApproval>> ApproveLoanModule(PendingApproval pendingApproval, string approverUsername, bool? approvalValue);
        public Task<ServiceResponse<PendingApproval>> ApproveSavingsModule(PendingApproval pendingApproval, string approverUsername, bool? approvalValue);
        public Task<ServiceResponse<PendingApproval>> ApproveCashAdditionModule(PendingApproval pendingApproval, string approverUsername, bool? approvalValue);
        public Task<ServiceResponse<PendingApproval>> ApproveWaiversModule(PendingApproval pendingApproval, string approverUsername, bool? approvalValue);
        public Task<ServiceResponse<PendingApproval>> ApproveLoanOffsetsModule(PendingApproval pendingApproval, string approverUsername, bool? approvalValue);
        public Task<ServiceResponse<PendingApproval>> ApproveTransferModule(PendingApproval pendingApproval, string approverUsername, bool? approvalValue);

    }
}
