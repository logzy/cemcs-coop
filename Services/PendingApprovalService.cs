using COOP.Banking.BusinessEntities;
using COOP.Banking.Data;
using COOP.Banking.Data.DTO;
using COOP.Banking.Services.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COOP.Banking.Services
{
    class PendingApprovalService : IPendingApprovalService<ModuleApprover>
    {
        private readonly CoopBankingDataContext _context;
        private readonly IMemberService _memberService;
        private readonly ILoanService _loanService;
        private readonly IMemberSavingService _memberSavingService;
        private readonly ISavingDepositTransactionService _savingDepositTransactionService;
        private readonly ISavingDepositLedgerService _savingDepositLedgerService;
        private readonly IMemberBalanceService _memberBalanceService;
        private readonly IModuleApproverService _moduleApproverService;
        private readonly IEmailSender _emailSender;

        public PendingApprovalService(CoopBankingDataContext context,
            IMemberService memberService,
            ILoanService loanService,
            IMemberSavingService memberSavingService,
            ISavingDepositTransactionService savingDepositTransactionService,
            ISavingDepositLedgerService savingDepositLedgerService,
            IMemberBalanceService memberBalanceService,
            IModuleApproverService moduleApproverService,
            IEmailSender emailSender)
        {
            _context = context;
            _memberService = memberService;
            _loanService = loanService;
            _emailSender = emailSender;
            _memberSavingService = memberSavingService;
            _savingDepositTransactionService = savingDepositTransactionService;
            _savingDepositLedgerService = savingDepositLedgerService;
            _memberBalanceService = memberBalanceService;
            _moduleApproverService = moduleApproverService;
        }

        public async Task<PendingApproval> CreateApproval(ModuleApprover moduleApprover, int itemId)
        {
            PendingApproval pendingApproval = new PendingApproval
            {
                ModuleApproverId = moduleApprover.Id,
                ItemId = itemId,
                Approved = null,
                CurrentLevel = 0
            };

            _context.PendingApprovals.Add(pendingApproval);
            await _context.SaveChangesAsync();
            return pendingApproval;
        }

        public async Task<List<PendingApproval>> GetPendingApprovals()
        {
            var pendingApprovals = await _context.PendingApprovals
                .Include(p => p.ModuleApprover)
                .ThenInclude(p => p.Module)
                .Include(p => p.ModuleApprover.ModuleApproverNameStores)
                .ToListAsync();
            return pendingApprovals;
        }

        public async Task<PendingApproval> GetPendingApproval(int pendingApprovalId)
        {
            var pendingApproval = await _context.PendingApprovals
                .Where(p => p.Id == pendingApprovalId)
                .Include(p => p.ModuleApprover)
                .ThenInclude(p => p.Module)
                .Include(p => p.ModuleApprover.ModuleApproverNameStores)
                .FirstOrDefaultAsync();
            return pendingApproval;
        }

        public async Task<IList<PendingApproval>> GetUserPendingApproval(string username)
        {
            var pendingApprovals = await _context.PendingApprovals
                .Include(p => p.ModuleApprover)
                .ThenInclude(p => p.Module)
                .Include(p => p.ModuleApprover.ModuleApproverNameStores)
                .Where(p => p.ModuleApprover.ModuleApproverNameStores.ToList()[p.CurrentLevel].Usernames.Contains(username))
                .ToListAsync();
            return pendingApprovals;
        }


        public async Task<ServiceResponse<PendingApproval>> ApproveMembershipModule(PendingApproval pendingApproval, string approverUsername, bool? approvalValue)
        {
            var usersList = pendingApproval.ModuleApprover.ModuleApproverNameStores.ToList();
            var member = await _memberService.GetMember(pendingApproval.ItemId);
            if (member == null)
                return new ServiceResponse<PendingApproval> { Data = null, Success = false, Message = "Member not found!" };
            // Check if user can approve the module
            if (!usersList.Where(x => x.ApprovalLevel == member.ApprovalCount + 1).First().Usernames.Contains(approverUsername))
                return new ServiceResponse<PendingApproval> { Success = false, Message = $"User \"{approverUsername}\" cannot approve this {pendingApproval.ModuleApprover.Module.Name}" };
            
            StringBuilder message = new StringBuilder();

            if (approvalValue == true)
            {

                member.ApprovalCount++;
                pendingApproval.CurrentLevel++;
                if (member.ApprovalCount == pendingApproval.ModuleApprover.Level)
                {
                    member.Active = true;
                    member.Approved = true;
                    pendingApproval.Approved = true;
                    var membersavings = await _memberSavingService.GetSavingsByMemId(member.Id);
                    var memBal1 = new MemberBalance
                    {
                        MemberId = member.Id,
                        BalanceType = Enums.BalanceType.MemberSavings,
                        CurrentBalance = 0,
                        ItemId = (int)Enums.SavingsType.savings
                        //SavingsAmount = 
                    };
                    var memBal2 = new MemberBalance
                    {
                        MemberId = member.Id,
                        BalanceType = Enums.BalanceType.MemberSavings,
                        CurrentBalance = 0,
                        ItemId = (int)Enums.SavingsType.deposit
                    };
                    _context.AddRange(memBal1, memBal2);
                }
                _context.Attach(member).State = EntityState.Modified;


                //APPROVAL NOTIFICATION
                //bool? emailSent = null;
                message.Append($"<p>Dear {member.Person.FirstName}</p>");
                if (pendingApproval.Approved == false)
                {
                    message.Append($"<p>Please be informed that your Membership Application has gone through {pendingApproval.CurrentLevel} approved(s).</p>");
                    message.Append($"<p>You have {pendingApproval.ModuleApprover.Level - pendingApproval.CurrentLevel} Approval(s) left. Goodluck!</p>");
                }
                else
                {
                    message.Append($"<p>Please be informed that your Membership Application has passed its final approval stage.</p>");
                    //message.Append($"<p>It is now being processed for payment.</p>");
                }
            }
            else if (approvalValue == false)
            {
                pendingApproval.Approved = false;
                message.Append($"<p>Dear {member.Person.FirstName}</p>");
                message.Append($"<p>Please be informed that your Membership Application has been rejected.</p>");
            }

            try
            {
                await _emailSender.SendEmailAsync(member.Person.Email, "CEMCS Loan Approval", message.ToString());
            }
            catch (Exception) { }
            _context.Attach(pendingApproval).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return new ServiceResponse<PendingApproval> { Data = pendingApproval, Success = true, Message = $"Membership Registeration Approved by {approverUsername}" }; ;
        }

        public async Task<ServiceResponse<PendingApproval>> ApproveLoanModule(PendingApproval pendingApproval, string approverUsername, bool? approvalValue)
        {
            var usersList = pendingApproval.ModuleApprover.ModuleApproverNameStores.ToList();
            var loanApplication = await _loanService.GetLoanDataById(pendingApproval.ItemId);
            if (loanApplication == null)
                return new ServiceResponse<PendingApproval> { Data = null, Success = false, Message = "Member not found!" };
            // Check if user can approve the module
            if (!usersList.Where(x => x.ApprovalLevel == loanApplication.ApprovalCount + 1).First().Usernames.Contains(approverUsername))
                return new ServiceResponse<PendingApproval> { Success = false, Message = $"User \"{approverUsername}\" cannot approve this {pendingApproval.ModuleApprover.Module.Name}" };

            StringBuilder message = new StringBuilder();

            if (approvalValue == true)
            {
                loanApplication.ApprovalCount++;
                pendingApproval.CurrentLevel++;
                if (loanApplication.ApprovalCount == pendingApproval.ModuleApprover.Level)
                {
                    loanApplication.Approved = true;
                    loanApplication.LoanCondition = Enums.LoanConditionStatus.Running;
                    pendingApproval.Approved = true;
                }
                _context.Attach(loanApplication).State = EntityState.Modified;


                //APPROVAL NOTIFICATION
                //bool? emailSent = null;
                message.Append($"<p>Dear {loanApplication.Member.Person.FirstName}</p>");
                if (pendingApproval.Approved == false)
                {
                    message.Append($"<p>Please be informed that your {loanApplication.LoanAmount} Loan has gone through {pendingApproval.CurrentLevel} approved(s).</p>");
                    message.Append($"<p>You have {pendingApproval.ModuleApprover.Level - pendingApproval.CurrentLevel} Approval(s) left. Goodluck!</p>");
                }
                else
                {
                    message.Append($"<p>Please be informed that your {loanApplication.LoanAmount} Loan has passed its final approval stage.</p>");
                    message.Append($"<p>It is now being processed for payment.</p>");
                }
            }
            else if (approvalValue == false)
            {
                pendingApproval.Approved = false;
                message.Append($"<p>Dear {loanApplication.Member.Person.FirstName}</p>");
                message.Append($"<p>Please be informed that your {loanApplication.LoanAmount} Loan has been rejected.</p>");
            }

            try
            {
                await _emailSender.SendEmailAsync(loanApplication.Member.Person.Email, "CEMCS Loan Approval", message.ToString());
            }
            catch (Exception) { }

            await _context.SaveChangesAsync();
            _context.Attach(pendingApproval).State = EntityState.Modified;

            return new ServiceResponse<PendingApproval> { Data = pendingApproval, Success = true, Message = $"Loan Application Approved by {approverUsername}" };
        }

        public async Task<ServiceResponse<PendingApproval>> ApproveSavingsModule(PendingApproval pendingApproval, string approverUsername, bool? approvalValue)
        {
            var usersList = pendingApproval.ModuleApprover.ModuleApproverNameStores.ToList();

            var savingsDeposit = await _savingDepositTransactionService.GetSavingDepositTransaction(pendingApproval.ItemId);
            if (savingsDeposit == null)
                return new ServiceResponse<PendingApproval> { Data = null, Success = false, Message = "Member Savings not found!" };

            // Check if user can approve the module
            if (!usersList.Where(x => x.ApprovalLevel == savingsDeposit.ApprovalCount + 1).First().Usernames.Contains(approverUsername))
                return new ServiceResponse<PendingApproval> { Success = false, Message = $"User \"{approverUsername}\" cannot approve this {pendingApproval.ModuleApprover.Module.Name}" };

            StringBuilder message = new StringBuilder();

            if (approvalValue == true) 
            {
                savingsDeposit.ApprovalCount++;
                savingsDeposit.LastApprovedBy = approverUsername;
                savingsDeposit.LastApprovalDate = DateTime.UtcNow;

                pendingApproval.CurrentLevel++;
                if (savingsDeposit.ApprovalCount == pendingApproval.ModuleApprover.Level)
                {

                    // Get member saving
                    List<MemberSaving> memberSavings = await _memberSavingService.GetSavingsByMemberIdAndType(savingsDeposit.MemberId, savingsDeposit.SavingsType);
                    var selectMemberSaving = memberSavings.Where(x => x.Type == savingsDeposit.SavingsType).FirstOrDefault();
                    // For savings increase and decrease

                    selectMemberSaving.SavingsAmount = savingsDeposit.DepositAmount;
                    _context.Attach(selectMemberSaving).State = EntityState.Modified;

                    savingsDeposit.Status = (int)Enums.ApprovalStatus.Approved;
                    savingsDeposit.Approved = true;
                    savingsDeposit.Approved = true;
                    pendingApproval.Approved = true;
                }
                _context.Attach(savingsDeposit).State = EntityState.Modified;

                //APPROVAL NOTIFICATION
                //bool? emailSent = null;
                message.Append($"<p>Dear {savingsDeposit.Member.Person.FirstName}</p>");
                if (pendingApproval.Approved == false)
                {
                    message.Append($"<p>Please be informed that your {savingsDeposit.DepositAmount} Savings increase/decrease has gone through {pendingApproval.CurrentLevel} approved(s).</p>");
                    message.Append($"<p>You have {pendingApproval.ModuleApprover.Level - pendingApproval.CurrentLevel} Approval(s) left. Goodluck!</p>");
                }
                else
                {
                    message.Append($"<p>Please be informed that your {savingsDeposit.DepositAmount} Savings increase/decrease has passed its final approval stage.</p>");
                    message.Append($"<p>It is now being processed for payment.</p>");
                }
            }
            else if (approvalValue == false)
            {
                pendingApproval.Approved = false;
                message.Append($"<p>Dear {savingsDeposit.Member.Person.FirstName}</p>");
                message.Append($"<p>Please be informed that your {savingsDeposit.DepositAmount} Savings increase/decrease has been rejected.</p>");
            }

            try
            {
                await _emailSender.SendEmailAsync(savingsDeposit.Member.Person.Email, "CEMCS Savings increase/decrease Approval", message.ToString());
            }
            catch (Exception) { }

            _context.Attach(pendingApproval).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return new ServiceResponse<PendingApproval> { Data = pendingApproval, Success = true, Message = $"Member Savings Approved  by {approverUsername}" }; ;
        }

        public async Task<ServiceResponse<PendingApproval>> ApproveCashAdditionModule(PendingApproval pendingApproval, string approverUsername, bool? approvalValue)
        {
            var usersList = pendingApproval.ModuleApprover.ModuleApproverNameStores.ToList();

            var savingsDeposit = await _savingDepositTransactionService.GetSavingDepositTransaction(pendingApproval.ItemId);
            if (savingsDeposit == null)
                return new ServiceResponse<PendingApproval> { Data = null, Success = false, Message = "Member Savings not found!" };

            // Check if user can approve the module
            if (!usersList.Where(x => x.ApprovalLevel == savingsDeposit.ApprovalCount + 1).First().Usernames.Contains(approverUsername))
                return new ServiceResponse<PendingApproval> { Success = false, Message = $"User \"{approverUsername}\" cannot approve this {pendingApproval.ModuleApprover.Module.Name}" };

            StringBuilder message = new StringBuilder();

            if (approvalValue == true) 
            {
                savingsDeposit.ApprovalCount++;
                savingsDeposit.LastApprovedBy = approverUsername;
                savingsDeposit.LastApprovalDate = DateTime.UtcNow;

                pendingApproval.CurrentLevel++;

                if (savingsDeposit.ApprovalCount == pendingApproval.ModuleApprover.Level)
                {
                    // Get member saving
                    List<MemberSaving> memberSavings = await _memberSavingService.GetSavingsByMemberIdAndType(savingsDeposit.MemberId, savingsDeposit.SavingsType);
                    var selectMemberSaving = memberSavings.Where(x => x.Type == savingsDeposit.SavingsType).FirstOrDefault();

                    //For Cash Addition
                    var memberBalance = await _memberBalanceService.GetMemberBalanceByItemId(selectMemberSaving.Id);
                    SavingDepositLedger newLedger = new SavingDepositLedger
                    {
                        MemberId = savingsDeposit.MemberId,
                        SavingsType = savingsDeposit.SavingsType,
                        TransactionDate = savingsDeposit.TransactionDate,
                        PreviousBalance = memberBalance.CurrentBalance,
                        DepositAmount = savingsDeposit.DepositAmount,
                        CurrentBalance = savingsDeposit.DepositAmount + memberBalance.CurrentBalance,
                        Status = (int)Enums.ApprovalStatus.Pending,
                        TransactionTypeId = savingsDeposit.TransactionTypeId,
                    };
                    // memberBalance.CurrentBalance = newLedger.CurrentBalance;
                    _context.SavingDepositLedgers.Add(newLedger);
                    // _context.Attach(memberBalance).State = EntityState.Modified;

                    savingsDeposit.Status = (int)Enums.ApprovalStatus.Approved;
                    savingsDeposit.DueDate = DateTime.UtcNow.AddMonths(1);
                    savingsDeposit.HasReflected = false;
                    savingsDeposit.Approved = true;
                    savingsDeposit.Approved = true;
                    pendingApproval.Approved = true;
                }
                _context.Attach(savingsDeposit).State = EntityState.Modified;


                //APPROVAL NOTIFICATION
                //bool? emailSent = null;
                message.Append($"<p>Dear {savingsDeposit.Member.Person.FirstName}</p>");
                if (pendingApproval.Approved == false)
                {
                    message.Append($"<p>Please be informed that your {savingsDeposit.DepositAmount} Cash Addition has gone through {pendingApproval.CurrentLevel} approved(s).</p>");
                    message.Append($"<p>You have {pendingApproval.ModuleApprover.Level - pendingApproval.CurrentLevel} Approval(s) left. Goodluck!</p>");
                }
                else
                {
                    message.Append($"<p>Please be informed that your {savingsDeposit.DepositAmount} Cash Addition has passed its final approval stage.</p>");
                    message.Append($"<p>It is now being processed for payment.</p>");
                }
            }
            else if (approvalValue == false)
            {
                pendingApproval.Approved = false;
                message.Append($"<p>Dear {savingsDeposit.Member.Person.FirstName}</p>");
                message.Append($"<p>Please be informed that your {savingsDeposit.DepositAmount} Cash Addition has been rejected.</p>");
            }

            try
            {
                await _emailSender.SendEmailAsync(savingsDeposit.Member.Person.Email, "CEMCS Cash Addition Approval", message.ToString());
            }
            catch (Exception) { }

            _context.Attach(pendingApproval).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return new ServiceResponse<PendingApproval> { Data = pendingApproval, Success = true, Message = $"Member Cash Addition Approved by {approverUsername}" }; ;
        }

        public async Task<ServiceResponse<PendingApproval>> ApproveWaiversModule(PendingApproval pendingApproval, string approverUsername, bool? approvalValue)
        {
            var usersList = pendingApproval.ModuleApprover.ModuleApproverNameStores.ToList();

            return new ServiceResponse<PendingApproval> { Data = null, Success = true, Message = $"Waiver Approved by {approverUsername}" }; ;
        }

        public async Task<ServiceResponse<PendingApproval>> ApproveLoanOffsetsModule(PendingApproval pendingApproval, string approverUsername, bool? approvalValue)
        {
            var usersList = pendingApproval.ModuleApprover.ModuleApproverNameStores.ToList();

            return new ServiceResponse<PendingApproval> { Data = null, Success = true, Message = $"Loan Offset Approved by {approverUsername}" }; ;
        }

        public async Task<ServiceResponse<PendingApproval>> ApproveTransferModule(PendingApproval pendingApproval, string approverUsername, bool? approvalValue)
        {
            var usersList = pendingApproval.ModuleApprover.ModuleApproverNameStores.ToList();

            return new ServiceResponse<PendingApproval> { Data = null, Success = true, Message = $"Member Savings Approved by {approverUsername}" }; ;
        }

        public async Task<List<PendingApprovalRequestDTO<Member>>> GetUserPendingMemberApprovals(string username)
        {
            // Get module approver for membership
            Module module = await _moduleApproverService.GetModuleByNormalizedName("MEMBERSHIPS");

            var pendingApprovals = await _context.PendingApprovals
                .Include(p => p.ModuleApprover)
                .ThenInclude(p => p.Module)
                .Include(p => p.ModuleApprover.ModuleApproverNameStores)
                .Where(p => p.ModuleApprover.ModuleId == module.Id && p.Approved == null)
                .ToListAsync();
            pendingApprovals = pendingApprovals.Where(p => p.ModuleApprover.ModuleApproverNameStores.ToList()[p.CurrentLevel].Usernames.Contains(username)).ToList();
            List<PendingApprovalRequestDTO<Member>> pendingApprovalRequests = new List<PendingApprovalRequestDTO<Member>>();
            foreach (var item in pendingApprovals)
            {
                pendingApprovalRequests.Add(new PendingApprovalRequestDTO<Member>
                {
                    Id = item.Id,
                    ModuleApproverId = item.ModuleApproverId,
                    ItemId = item.ItemId,
                    Approved = item.Approved,
                    ModuleApprover = item.ModuleApprover,
                    ItemData = await _memberService.GetMember(item.ItemId)
                });
            }
            return pendingApprovalRequests;
        }

        public async Task<List<PendingApprovalRequestDTO<LoanApplication>>> GetUserPendingLoanApprovals(string username)
        {
            // Get module approver for loans
            Module module = await _moduleApproverService.GetModuleByNormalizedName("LOANS");

            var pendingApprovals = await _context.PendingApprovals
                .Include(p => p.ModuleApprover)
                .ThenInclude(p => p.Module)
                .Include(p => p.ModuleApprover.ModuleApproverNameStores)
                .Where(p => p.ModuleApprover.ModuleId == module.Id && p.Approved == null)
                .ToListAsync();
            pendingApprovals = pendingApprovals.Where(p => p.ModuleApprover.ModuleApproverNameStores.ToList()[p.CurrentLevel].Usernames.Contains(username)).ToList();
            List<PendingApprovalRequestDTO<LoanApplication>> pendingApprovalRequests = new List<PendingApprovalRequestDTO<LoanApplication>>();
            foreach (var item in pendingApprovals)
            {
                pendingApprovalRequests.Add(new PendingApprovalRequestDTO<LoanApplication>
                {
                    Id = item.Id,
                    ModuleApproverId = item.ModuleApproverId,
                    ItemId = item.ItemId,
                    Approved = item.Approved,
                    ModuleApprover = item.ModuleApprover,
                    ItemData = await _loanService.GetLoanDataById(item.ItemId)
                });
            }
            return pendingApprovalRequests;
        }

        public async Task<List<PendingApprovalRequestDTO<SavingDepositTransaction>>> GetUserPendingSavingApprovals(string username)
        {
            // Get module approver for savings increase/decrease
            Module module = await _moduleApproverService.GetModuleByNormalizedName("SAVINGS");

            var pendingApprovals = await _context.PendingApprovals
                .Include(p => p.ModuleApprover)
                .ThenInclude(p => p.Module)
                .Include(p => p.ModuleApprover.ModuleApproverNameStores)
                .Where(p => p.ModuleApprover.ModuleId == module.Id && p.Approved == null)
                .ToListAsync();
            pendingApprovals = pendingApprovals.Where(p => p.ModuleApprover.ModuleApproverNameStores.ToList()[p.CurrentLevel].Usernames.Contains(username)).ToList();

            List<PendingApprovalRequestDTO<SavingDepositTransaction>> pendingApprovalRequests = new List<PendingApprovalRequestDTO<SavingDepositTransaction>>();
            foreach (var item in pendingApprovals)
            {
                pendingApprovalRequests.Add(new PendingApprovalRequestDTO<SavingDepositTransaction>
                {
                    Id = item.Id,
                    ModuleApproverId = item.ModuleApproverId,
                    ItemId = item.ItemId,
                    Approved = item.Approved,
                    ModuleApprover = item.ModuleApprover,
                    ItemData = await _savingDepositTransactionService.GetSavingDepositTransaction(item.ItemId)
                });
            }
            return pendingApprovalRequests;
        }

        public async Task<List<PendingApprovalRequestDTO<SavingDepositTransaction>>> GetUserPendingCashApprovals(string username)
        {
            // Get module approver for cash addition
            Module module = await _moduleApproverService.GetModuleByNormalizedName("CASH-ADDITION");

            var pendingApprovals = await _context.PendingApprovals
                .Include(p => p.ModuleApprover)
                .ThenInclude(p => p.Module)
                .Include(p => p.ModuleApprover.ModuleApproverNameStores)
                .Where(p => p.ModuleApprover.ModuleId == module.Id && p.Approved == null)
                .ToListAsync();
            pendingApprovals = pendingApprovals.Where(p => p.ModuleApprover.ModuleApproverNameStores.ToList()[p.CurrentLevel].Usernames.Contains(username)).ToList();

            List<PendingApprovalRequestDTO<SavingDepositTransaction>> pendingApprovalRequests = new List<PendingApprovalRequestDTO<SavingDepositTransaction>>();
            foreach (var item in pendingApprovals)
            {
                pendingApprovalRequests.Add(new PendingApprovalRequestDTO<SavingDepositTransaction>
                {
                    Id = item.Id,
                    ModuleApproverId = item.ModuleApproverId,
                    ItemId = item.ItemId,
                    Approved = item.Approved,
                    ModuleApprover = item.ModuleApprover,
                    ItemData = await _savingDepositTransactionService.GetSavingDepositTransaction(item.ItemId)
                });
            }
            return pendingApprovalRequests;
        }
    }
}
