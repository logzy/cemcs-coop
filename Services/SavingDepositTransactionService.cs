using COOP.Banking.BusinessEntities;
using COOP.Banking.Data;
using COOP.Banking.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COOP.Banking.Services
{

    class SavingDepositTransactionService : ISavingDepositTransactionService
    {
        private readonly CoopBankingDataContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMemberService _memberService;
        private readonly IEmailSender _emailSender;
        private readonly IModuleApproverService _moduleApproverService;
        public SavingDepositTransactionService(CoopBankingDataContext context, UserManager<ApplicationUser> userManager,
            IMemberService memberService, IEmailSender emailSender, IModuleApproverService moduleApproverService)
        {
            _context = context;
            _userManager = userManager;
            _memberService = memberService;
            _emailSender = emailSender;
            _moduleApproverService = moduleApproverService;
        }


        public async Task<SavingDepositTransaction> GetSavingDepositTransaction(int savingDepositTransactionId)
        {
            var savingDepositTransaction = await _context.SavingDepositTransactions
                .Where(s => s.Id == savingDepositTransactionId)
                .Include(s => s.Member)
                .Include(s => s.TransactionType)
                .FirstOrDefaultAsync();
            return savingDepositTransaction;
        }

        public async Task<SavingDepositTransaction> GetSavingDepositTransactionByTag(string tag)
        {
            var savingDepositTransaction = await _context.SavingDepositTransactions
                .Where(s => s.Tag == tag)
                .Include(s => s.Member)
                .Include(s => s.TransactionType)
                .FirstOrDefaultAsync();
            return savingDepositTransaction;
        }

        public async Task<List<TransactionType>> GetTransactionTypes()
        {
            var transactionTypes = await _context.TransactionTypes
                .ToListAsync();
            return transactionTypes;
        }

        public async Task<List<SavingDepositTransaction>> GetSavingDepositTransactions()
        {
            var savingDepositTransactions = await _context.SavingDepositTransactions
                .Include(s => s.Member)
                .Include(s => s.TransactionType)
                .ToListAsync();
            return savingDepositTransactions;
        }
        public async Task<SavingDepositTransaction> SaveSavingDepositTransaction(SavingDepositTransaction savingDepositTransaction)
        {
            _context.SavingDepositTransactions.Add(savingDepositTransaction);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return savingDepositTransaction;
        }

        public async Task<SavingDepositTransaction> UpdateSavingDepositTransaction(SavingDepositTransaction savingDepositTransaction)
        {
            _context.Attach(savingDepositTransaction).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return savingDepositTransaction;
        }

        public async Task<WithdrawalApplication> SaveWithdrawalApplication(WithdrawalApplication application, string UserId)
        {
            _context.WithdrawalApplications.Add(application);
            try
            {

                await _context.SaveChangesAsync();

                var member = await _memberService.GetMemberByUserId(UserId);

                StringBuilder builder = new StringBuilder();


                builder.AppendLine(string.Format("<p>Dear {0},</p>", member.Person.FirstName));
                builder.AppendLine(string.Format("<p>Your Fund Withdrawal application has been submitted</p>"));
                builder.AppendLine(string.Format("<p>It is presently undergoing processing.</p>"));
                builder.AppendLine(string.Format("<p>Your Bank for this application:{0} {1}</p>", member.Person.FirstName, member.Person.FirstName));
                builder.AppendLine(string.Format("<p>Your Bank Account No. for this application:  {0}</p>", application.AccountNumber));
                builder.AppendLine(string.Format("<p>You will receive emails as soon as the required processes are completed</p>"));
                try
                {
                    await _emailSender.SendEmailAsync(member.Person.Email, "SPECIAL WITHDRAWABLE DEPOSIT SCHEME - FUND WITHHDRAWAL", builder.ToString());
                }
                catch (Exception)
                {
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return application;
        }

        public async Task<ServiceResponse<WithdrawalApplication>> SaveWithdrawal(WithdrawalApplication application, string UserId)
        {
            ServiceResponse<WithdrawalApplication> response = new ServiceResponse<WithdrawalApplication>();
            List<Error> errors = new List<Error>();
            Module module = new Module();
            module = await _moduleApproverService.GetModuleByNormalizedName("WITHDRAWAL");
            ModuleApprover moduleApprover = await _moduleApproverService.GetModuleApproverByModuleId(module.Id);
            if (moduleApprover == null)
            {

                Error error = new Error();
                error.ErrorCode = Enums.ApplicatonResponseCodes.ApproversNotSetup.ToString();
                error.ErrorMessage = "No module approver registered";
                errors.Add(error);
            }
            else
            {
                //get the sp balance of the user
                var MemberBalances = await _context.MemberBalances.Where(x => x.MemberId == application.MemberId).ToListAsync();
                if (MemberBalances != null && MemberBalances.Count > 0)
                {

                    var DepositBalance = MemberBalances.FirstOrDefault(x => x.BalanceType.Equals(Enums.BalanceType.MemberSavings) && x.ItemId == (int)Enums.SavingsType.deposit);
                    if (DepositBalance != null)
                    {
                        var SavingsDeposii = DepositBalance.CurrentBalance;
                        if (SavingsDeposii < application.Amount)
                        {
                            Error error = new Error();
                            error.ErrorCode = Enums.ApplicatonResponseCodes.InvalidBalance.ToString();
                            error.ErrorMessage = "There's not enough balance to process this withdrawal";
                        }
                        else
                        {
                            _context.WithdrawalApplications.Add(application);
                            try
                            {

                                await _context.SaveChangesAsync();
                                response.Data = application;
                                response.Success = true;
                                response.Message = "Operation successful";
                                var member = await _memberService.GetMemberByUserId(UserId);

                                StringBuilder builder = new StringBuilder();


                                builder.AppendLine(string.Format("<p>Dear {0},</p>", member.Person.FirstName));
                                builder.AppendLine(string.Format("<p>Your Fund Withdrawal application has been submitted</p>"));
                                builder.AppendLine(string.Format("<p>It is presently undergoing processing.</p>"));
                                builder.AppendLine(string.Format("<p>Your Bank for this application:{0} {1}</p>", member.Person.FirstName, member.Person.FirstName));
                                builder.AppendLine(string.Format("<p>Your Bank Account No. for this application:  {0}</p>", application.AccountNumber));
                                builder.AppendLine(string.Format("<p>You will receive emails as soon as the required processes are completed</p>"));
                                try
                                {
                                    await _emailSender.SendEmailAsync(member.Person.Email, "SPECIAL WITHDRAWABLE DEPOSIT SCHEME - FUND WITHHDRAWAL", builder.ToString());
                                }
                                catch (Exception)
                                {
                                }


                            }
                            catch (Exception ex)
                            {
                                Error error = new Error();
                                error.ErrorCode = Enums.ApplicatonResponseCodes.InvalidBalance.ToString();
                                error.ErrorMessage = ex.Message;
                                errors.Add(error);
                            }
                        }
                    }
                    else
                    {
                        Error error = new Error();
                        error.ErrorCode = Enums.ApplicatonResponseCodes.InvalidBalance.ToString();
                        error.ErrorMessage = "You have no special deposit balance";
                    }
                }
                else
                {
                    Error error = new Error();
                    error.ErrorCode = Enums.ApplicatonResponseCodes.InvalidBalance.ToString();
                    error.ErrorMessage = "You have no balances";
                    errors.Add(error);
                }
            }
            response.Errors = errors;
            if (errors != null && errors.Count > 0)
            {
                response.Message = "Operation failed";
                response.Success = false;
            }
            else
            {
                response.Message = "Operation successful";
                response.Success = true;
            }


            return response;
        }

        public async Task<ServiceResponse<TransferApplication>> SaveTransfer(TransferApplication application, string UserId)
        {
            ServiceResponse<TransferApplication> response = new ServiceResponse<TransferApplication>();
            List<Error> errors = new List<Error>();
            Module module = new Module();
            module = await _moduleApproverService.GetModuleByNormalizedName("TRANSFER");
            ModuleApprover moduleApprover = await _moduleApproverService.GetModuleApproverByModuleId(module.Id);
            if (moduleApprover == null)
            {

                Error error = new Error();
                error.ErrorCode = Enums.ApplicatonResponseCodes.ApproversNotSetup.ToString();
                error.ErrorMessage = "No module approver registered";
                errors.Add(error);
            }
            else
            {
                //get the sp balance of the user
                var MemberBalances = await _context.MemberBalances.Where(x => x.MemberId == application.MemberId).ToListAsync();
                if (MemberBalances != null && MemberBalances.Count > 0)
                {

                    var DepositBalance = MemberBalances.FirstOrDefault(x => x.BalanceType.Equals(Enums.BalanceType.MemberSavings) && x.ItemId == (int)Enums.SavingsType.deposit);
                    if (DepositBalance != null)
                    {
                        var SavingsDeposii = DepositBalance.CurrentBalance;
                        if (SavingsDeposii < application.Amount)
                        {
                            Error error = new Error();
                            error.ErrorCode = Enums.ApplicatonResponseCodes.InvalidBalance.ToString();
                            error.ErrorMessage = "There's not enough balance to process this transfer request";
                        }
                        else
                        {
                            _context.TransferApplications.Add(application);
                            try
                            {

                                await _context.SaveChangesAsync();
                                response.Data = application;
                                response.Success = true;
                                response.Message = "Operation successful";
                                var member = await _memberService.GetMemberByUserId(UserId);

                                StringBuilder builder = new StringBuilder();


                                builder.AppendLine(string.Format("<p>Dear {0},</p>", member.Person.FirstName));
                                builder.AppendLine(string.Format("<p>Your Transfer application has been submitted</p>"));
                                builder.AppendLine(string.Format("<p>It is presently undergoing processing.</p>"));
                                builder.AppendLine(string.Format("<p>You will receive emails as soon as the required processes are completed</p>"));
                                try
                                {
                                    await _emailSender.SendEmailAsync(member.Person.Email, "SPECIAL WITHDRAWABLE DEPOSIT SCHEME - FUND TRANSFER", builder.ToString());
                                }
                                catch (Exception)
                                {
                                }


                            }
                            catch (Exception ex)
                            {
                                Error error = new Error();
                                error.ErrorCode = Enums.ApplicatonResponseCodes.InvalidBalance.ToString();
                                error.ErrorMessage = ex.Message;
                                errors.Add(error);
                            }
                        }
                    }
                    else
                    {
                        Error error = new Error();
                        error.ErrorCode = Enums.ApplicatonResponseCodes.InvalidBalance.ToString();
                        error.ErrorMessage = "You have no special deposit balance";
                    }
                }
                else
                {
                    Error error = new Error();
                    error.ErrorCode = Enums.ApplicatonResponseCodes.InvalidBalance.ToString();
                    error.ErrorMessage = "You have no balances";
                    errors.Add(error);
                }
            }
            response.Errors = errors;
            if (errors != null && errors.Count > 0)
            {
                response.Message = "Operation failed";
                response.Success = false;
            }
            else
            {
                response.Message = "Operation successful";
                response.Success = true;
            }


            return response;
        }
        public async Task<List<SavingDepositTransaction>> GetUnreflectedTransactions()
        {
            var savingDepositTransactions = await _context.SavingDepositTransactions
                .Where(s => s.SavingsType == (int)Enums.SavingsType.deposit && s.Approved && !(bool)s.HasReflected)
                .Include(s => s.Member)
                .Include(s => s.TransactionType)
                .ToListAsync();
            return savingDepositTransactions;
        }
    }
}
