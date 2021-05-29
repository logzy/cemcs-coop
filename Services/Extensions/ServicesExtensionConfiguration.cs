using COOP.Banking.BusinessEntities;
using COOP.Banking.Services.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace COOP.Banking.Services.Extensions
{
    public static class ServicesExtensionConfiguration
    {
        public static void AddServicesExtensions(this IServiceCollection services)
        {
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<ILoanService, LoanService>();
            services.AddScoped<ILoanConfigService, LoanConfigService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDeptService, DeptService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IPendingApprovalService<ModuleApprover>, PendingApprovalService>();
            services.AddScoped<IApproverService, ApproverService>();
            services.AddScoped<IModuleApproverService, ModuleApproverService>();
            services.AddScoped<IMemberSavingService, MemberSavingService>();
            services.AddScoped<ISavingsConfigurationService, SavingsConfigurationService>();
            services.AddScoped<ISavingDepositLedgerService, SavingDepositLedgerService>();
            services.AddScoped<ISavingDepositTransactionService, SavingDepositTransactionService>();
            services.AddScoped<IEmailSender, EmailSenderService>();
            services.AddScoped<IMemberBalanceService, MemberBalanceService>();
            services.AddScoped<IFlutterwaveService, FlutterwaveService>();
            services.AddScoped<IBankService, BankService>();
            services.AddScoped<IMigrationService, MigrationService>();
            services.AddScoped<IModeOfPaymentService, ModeOfPaymentService>();
            services.AddScoped<IBeneficiaryService, BeneficiaryService>();
        }
    }
}
