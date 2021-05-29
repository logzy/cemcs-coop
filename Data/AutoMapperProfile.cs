using AutoMapper;
using COOP.Banking.BusinessEntities;
using COOP.Banking.Data.DTO;
using System.Collections.Generic;

namespace COOP.Banking.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CreateMap<Member, MemberRegDTO>().ReverseMap();
            //CreateMap<Person, MemberRegDTO>().ReverseMap();
            //------Treats relationship between person and member-----//
            CreateMap<Member, MemberDTO>()
                .IncludeMembers(source => source.Person);
            CreateMap<MemberDTO, Member>()
                .IncludeMembers(source => source.Person);
            CreateMap<Person, MemberDTO>().ReverseMap();
            CreateMap<Person, Member>().ReverseMap();

            CreateMap<Member, GetMemEmpNumberDTO>();
            //-------------------------------------------------------//
            CreateMap<MemberSaving, MemberSavingDTO>().ReverseMap();
            CreateMap<SavingsConfiguration, SavingsConfigurationDTO>().ReverseMap();
            //----------------------------------------------------------//
            CreateMap<Person, PersonDTO>().ReverseMap();

            CreateMap<Employee, EmployeeGetDTO>().ReverseMap();
            CreateMap<Employee, EmployeeRegDTO>().ReverseMap();

            CreateMap<State, StateDTO>().ReverseMap();

            CreateMap<LoanDTO, Loan>().ReverseMap();

            CreateMap<LoanConfigDTO, LoanConfig>().ReverseMap();

            CreateMap<Department, DeptDTO>().ReverseMap();

            CreateMap<EmployeeType, EmployeeTypeDTO>().ReverseMap();

            CreateMap<CreditCommittee, CreditCommitteeDTO>().ReverseMap();
            CreateMap<RegistrationFee, RegistrationFeeDTO>().ReverseMap();

            CreateMap<Approver, ApproverDTO>().ReverseMap();
            CreateMap<Loan, LoanApplicationDTO>().ReverseMap();

            //====== Module Approver ==========//
            CreateMap<ModuleApprover, ModuleApproverDTO>()
                .IncludeMembers(source => source.Module, source => source.ModuleApproverNameStores);
            CreateMap<ModuleApproverDTO, ModuleApprover>()
                .IncludeMembers(source => source.Module, source => source.ModuleApproverNameStores);
            CreateMap<Module, ModuleApprover>(); ;
            CreateMap<Module, ModuleApproverDTO>(); ;
            CreateMap<ICollection<ModuleApproverNameStore>, ModuleApproverDTO>();
            CreateMap<ICollection<ModuleApproverNameStore>, ModuleApprover>();

            CreateMap<LoanApplication, LoanApplicationDTO>().ReverseMap();
            CreateMap<LoanApplication, LoanPostDTO>().ReverseMap();
            CreateMap<LoanApplication, MimeLoanPostDTO>().ReverseMap();
            CreateMap<LoanRepayment, RepaymentDTO>().ReverseMap();
            CreateMap<LoanGuarantor, LoanGuarantorDTO>().ReverseMap();
            CreateMap<LoanGuarantor, LoanGuarantorPostDTO>().ReverseMap();

            // Pending approval
            CreateMap<PendingApproval, PendingApprovalDTO>().ReverseMap();

            //Saving Deposit Transaction & Ledger
            CreateMap<SavingDepositLedger, SavingDepositLedgerDTO>().ReverseMap();
            CreateMap<SavingDepositTransaction, SavingDepositTransactionDTO>().ReverseMap();

            CreateMap<MemberBalance, MemberBalanceDTO>().ReverseMap();
            CreateMap<Bank, BankDTO>().ReverseMap();
            CreateMap<ModeOfPayment, ModeOfPaymentDTO>().ReverseMap();

            CreateMap<WithdrawalApplication, WithdrawalSaveDTO>().ReverseMap();
            CreateMap<TransferApplication, TransferSaveDTO>().ReverseMap();
            CreateMap<LoanConfig, LoanConfigParamDTO>().ReverseMap();

            CreateMap<Payment, PaymentDTO>().ReverseMap();

        }
    }
}
