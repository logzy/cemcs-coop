using AutoMapper;
using COOP.Banking.BusinessEntities;
using COOP.Banking.Data;
using COOP.Banking.Data.DTO;
using COOP.Banking.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COOP.Banking.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class SavingDepositTransactionController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ISavingDepositLedgerService _savingDepositLedgerService;
        private readonly IPendingApprovalService<ModuleApprover> _pendingApprovalService;
        private readonly ISavingDepositTransactionService _savingDepositTransactionService;
        private readonly IModuleApproverService _moduleApproverService;
        private readonly IBankService _bankService;
        private readonly IMapper _mapper;

        public SavingDepositTransactionController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ISavingDepositLedgerService savingDepositLedgerService,
            ISavingDepositTransactionService savingDepositTransactionService,
            IPendingApprovalService<ModuleApprover> pendingApprovalService,
            IModuleApproverService moduleApproverService,
            IBankService bankService,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _savingDepositLedgerService = savingDepositLedgerService;
            _savingDepositTransactionService = savingDepositTransactionService;
            _pendingApprovalService = pendingApprovalService;
            _moduleApproverService = moduleApproverService;
            _mapper = mapper;
            _bankService = bankService;
        }

        // GET:SavingDepositTransactions/All
        [HttpGet("api/SavingDepositTransactions/All")]
        public async Task<ActionResult> GetTaskAsync()
        {
            var savingDepositTransactions = await _savingDepositTransactionService.GetSavingDepositTransactions();
            var savingDepositTransactionsDTO = _mapper.Map<List<SavingDepositTransactionDTO>>(savingDepositTransactions);
            return Ok(savingDepositTransactionsDTO);

        }

        // GET:SavingDepositTransactions/All
        [HttpGet("api/SavingDepositTransactions/TransactionTypes")]
        public async Task<ActionResult> GetTrxTypesAsync()
        {
            var transactionTypes = await _savingDepositTransactionService.GetTransactionTypes();
            return Ok(transactionTypes);

        }

        // GET: /SavingDepositTransactions/1
        [HttpGet("api/SavingDepositTransactions/{savingDepositTransactionId}")]
        public async Task<ActionResult> GetAsync(int savingDepositTransactionId)
        {
            var savingDepositTransaction = await _savingDepositTransactionService.GetSavingDepositTransaction(savingDepositTransactionId);
            var savingDepositTransactionDTO = _mapper.Map<SavingDepositTransactionDTO>(savingDepositTransaction);
            return Ok(savingDepositTransactionDTO);
        }
        // POST: /SavingDepositTransactions/Withdrawal
        [HttpPost("api/SavingDepositTransactions/Withdrawal")]
        public async Task<ActionResult> PostWithdrawalAsync([FromBody] WithdrawalSaveDTO WithdrawalSaveDTO)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var withdrawalApplication = _mapper.Map<WithdrawalApplication>(WithdrawalSaveDTO);
            var Bank = await _bankService.GetBankByCode(WithdrawalSaveDTO.BankCode);
            if (Bank != null)
            {
                withdrawalApplication.BankId = Bank.Id;
            }

            var SavedResult = await _savingDepositTransactionService.SaveWithdrawal(withdrawalApplication, currentUser.Id);
            return Ok(SavedResult);

        }
        // POST: /SavingDepositTransactions/Transfer
        [HttpPost("api/SavingDepositTransactions/Transfer")]
        public async Task<ActionResult> PostTransferAsync([FromBody] TransferSaveDTO TransferSaveDTO)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var TransferApplication = _mapper.Map<TransferApplication>(TransferSaveDTO);

            var SavedResult = await _savingDepositTransactionService.SaveTransfer(TransferApplication, currentUser.Id);
            return Ok(SavedResult);

        }
        // POST: /SavingDepositTransactions
        [HttpPost("api/SavingDepositTransactions")]
        public async Task<ActionResult> PostAsync([FromBody] SavingDepositTransactionDTO savingDepositTransactionDTO)
        {
            if (savingDepositTransactionDTO.Id == null)
            {
                // Check if Member has module approver
                // Get module approver for member
                Module module = new Module();
                if (savingDepositTransactionDTO.SavingsType == (int)Enums.SavingsType.savings)
                    module = await _moduleApproverService.GetModuleByNormalizedName("SAVINGS");
                else if (savingDepositTransactionDTO.SavingsType == (int)Enums.SavingsType.deposit)
                    module = await _moduleApproverService.GetModuleByNormalizedName("CASH-ADDITION");
                // Use module to get module approver
                ModuleApprover moduleApprover = await _moduleApproverService.GetModuleApproverByModuleId(module.Id);
                if (moduleApprover == null)
                    return BadRequest("no module approver registered");


                var savingDepositTrx = _mapper.Map<SavingDepositTransaction>(savingDepositTransactionDTO);
                await _savingDepositTransactionService.SaveSavingDepositTransaction(savingDepositTrx);

                // Get the transaction that has just been added
                var addedTransaction = await _savingDepositTransactionService.GetSavingDepositTransactionByTag(savingDepositTrx.Tag);

                // Insert into the pending approval table
                await _pendingApprovalService.CreateApproval(moduleApprover, addedTransaction.Id);
            }
            else
            {
                var savingDepositTransaction = await _savingDepositTransactionService.GetSavingDepositTransaction(savingDepositTransactionDTO.MemberId);
                if (savingDepositTransaction == null)
                    return NotFound("Saving Deposit Transaction not found");

                //savingDepositTransaction.MemberId = savingDepositTransactionDTO.MemberId;
                savingDepositTransaction.SavingsType = savingDepositTransactionDTO.SavingsType;
                savingDepositTransaction.TransactionDate = savingDepositTransaction.TransactionDate;
                savingDepositTransaction.DepositAmount = savingDepositTransactionDTO.DepositAmount;
                savingDepositTransaction.TransactionTypeId = savingDepositTransaction.TransactionTypeId;

                await _savingDepositTransactionService.UpdateSavingDepositTransaction(savingDepositTransaction);
            }
            return Ok(savingDepositTransactionDTO);
        }
    }
}
