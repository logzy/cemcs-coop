using COOP.Banking.BusinessEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COOP.Banking.Services.Interfaces
{
    public interface IBeneficiaryService
    {
        public Task<Beneficiary> SaveBeneficiary(Beneficiary beneficiary);
        public Task<List<Beneficiary>> GetBeneficiariesByMemberId(int MemeberId);
        public Task<LoanBeneficiary> AddLoanBeneficary(int BeneficiaryId, int LoanApplicationId);
    }
}
