using COOP.Banking.BusinessEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace COOP.Banking.Data
{
    public class CoopBankingDataContext : IdentityDbContext<ApplicationUser>
    {
        public CoopBankingDataContext(DbContextOptions<CoopBankingDataContext> options) : base(options)
        {

        }
        public DbSet<Approver> Approvers { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<ConcurrentLoan> ConcurrentLoans { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<Executive> Executives { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanConfig> LoanConfigs { get; set; }
        public DbSet<LoanOffsetModeOfPayment> LoanOffsetModeOfPayments { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberType> MemberTypes { get; set; }
        public DbSet<ModeOfPayment> ModeOfPayments { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<ModuleApprover> ModuleApprovers { get; set; }
        public DbSet<NextOfKin> NextOfKins { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<TargetLoanType> TargetLoanTypes { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<CreditCommittee> CreditCommittees { get; set; }
        public DbSet<RegistrationFee> RegistrationFees { get; set; }
        public DbSet<SavingsConfiguration> SavingsConfigurations { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<VoucherTransaction> VoucherTransactions { get; set; }
        public DbSet<VoucherTransactionDetail> VoucherTransactionDetails { get; set; }
        public DbSet<MemberSaving> MemberSavings { get; set; }
        public DbSet<LoanApplication> LoanApplications { get; set; }
        public DbSet<LoanRepayment> LoanRepayments { get; set; }
        public DbSet<SavingDepositLedger> SavingDepositLedgers { get; set; }
        public DbSet<SavingDepositTransaction> SavingDepositTransactions { get; set; }
        public DbSet<MethodOfCollection> MethodOfCollections { get; set; }

        public DbSet<PendingApproval> PendingApprovals { get; set; }
        public DbSet<LoanBeneficiary> LoanBeneficiaries { get; set; }
        public DbSet<MemberBalance> MemberBalances { get; set; }
        public DbSet<LoanGuarantor> LoanGuarantors { get; set; }
        public DbSet<LoanGuarantorConfig> LoanGuarantorConfigs { get; set; }

        public DbSet<Retireeexport> Retireeexport { get; set; }
        public DbSet<TransferApplication> TransferApplications { get; set; }
        public DbSet<WithdrawalApplication> WithdrawalApplications { get; set; }
        public DbSet<WaiverType> WaiverTypes { get; set; }
        public DbSet<WaiverApplicationDetail> WaiverApplicationDetails { get; set; }
        public DbSet<RetireeBalance> RetireeBalances { get; set; }
        public DbSet<CreditCommitteeBalance> CreditCommitteeBalances { get; set; }

        public DbSet<ModuleApproverNameStore> ModuleApproverNameStores { get; set; }
        public DbSet<EmployeeDetail> EmployeeDetails { get; set; }

        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
