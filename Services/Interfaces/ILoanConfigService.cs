using COOP.Banking.BusinessEntities;
using COOP.Banking.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COOP.Banking.Services.Interfaces
{
    public interface ILoanConfigService
    {
        public Task<List<LoanConfig>> GetLoanConfigs();
        public Task<LoanConfig> GetLoanConfigById(int Id);
        public Task<LoanConfig> GetLoanConfigByLoanTypeId(int LoanTypeId);
        public Task<LoanConfig> GetLoanConfigByLoanId(int LoanId);
        public Task<LoanConfig> GetLoanConfigByLoanIdAndMember(int LoanId, int memberTypeId);
        public Task<LoanConfigParamDTO> GetLoanParmsByLoanIdAndMember(int LoanId, int memberTypeId, int MemberId);
        public Task<List<LoanConfig>> GetLoanConfigsByMemType(int memType);
        public Task<LoanConfig> SaveLoanConfig(LoanConfig LConfig);
        public Task<LoanConfig> UpdateLoanConfig(LoanConfig LConfig);
        public Task<LoanPlannerDTO> GetLoanPlanner(LoanPlannerParam param, int MemberTypeId, int MemberId);
        public Task<int> GetGuarantorsCount(GuarantorParamsDTO param);

    }
}