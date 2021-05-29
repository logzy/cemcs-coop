using COOP.Banking.BusinessEntities;
using COOP.Banking.Data;
using COOP.Banking.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COOP.Banking.Services.Interfaces
{
    public interface ILoanService
    {
        public Task<ServiceResponse<List<LoanDTO>>> GetLoans();
        public Task<ServiceResponse<List<LoanApplicationDTO>>> GetLoansByMemberId(int MemberId);
        public Task<ServiceResponse<List<LoanApplicationDTO>>> GetLoansByEmployeeNumber(string EmployeeNumber);
        public Task<ServiceResponse<LoanApplicationDTO>> GetLoanById(int id);
        public Task<LoanApplication> GetLoanByTag(string tag);
        public Task<LoanApplication> GetLoanDataById(int id);
        public Task<ServiceResponse<List<LoanApplication>>> GetPaidLoanApplications();
        public Task<ServiceResponse<List<LoanApplication>>> GetUnpaidLoanApplications();
        public Task<ServiceResponse<Loan>> CreateLoan(LoanDTO newloan);
        public Task<ServiceResponse<List<RepaymentDTO>>> SaveLoanApplication(LoanPostDTO postData);
        public Task<ServiceResponse<List<RepaymentDTO>>> SaveMimeLoanApplication(MimeLoanPostDTO loanData);
        public Task<ServiceResponse<string>> ApproveLoanByGuarantor(GuarantorApproverDTO approver);
        public Task<ServiceResponse<List<LoanGuarantorDTO>>> GetGuarantors(int MemberId);
    }
}