using AutoMapper;
using COOP.Banking.BusinessEntities;
using COOP.Banking.Data;
using COOP.Banking.Data.DTO;
using COOP.Banking.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COOP.Banking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class MemberBalancesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMemberService _memberService;
        private readonly IMemberBalanceService _memberBalanceService;
        private readonly IMapper _mapper;
        private readonly ISavingDepositTransactionService _savingDepositTransactionService;
        private readonly IMemberSavingService _memberSavingService;
        public MemberBalancesController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IMemberService memberService,
            IMemberBalanceService memberBalanceService,
            IMemberSavingService memberSavingService,
            ISavingDepositTransactionService savingDepositTransactionService,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _memberService = memberService;
            _memberBalanceService = memberBalanceService;
            _memberSavingService = memberSavingService;
            _mapper = mapper;
            _savingDepositTransactionService = savingDepositTransactionService;
        }

        // GET: /MemberBalances/All
        [HttpGet("All")]
        public async Task<ActionResult> GetAsync()
        {
            var memberBalance = await _memberBalanceService.GetMemberBalances();
            var memberBalanceDTO = _mapper.Map<List<MemberBalanceDTO>>(memberBalance);
            return Ok(memberBalanceDTO);
        }

        // GET: MemberBalances/5
        [HttpGet("{memberBalanceId}")]
        public async Task<ActionResult> GetAsync(int memberBalanceId)
        {
            var memberBalance = await _memberBalanceService.GetMemberBalance(memberBalanceId);
            var memberBalanceDTO = _mapper.Map<MemberBalanceDTO>(memberBalance);
            return Ok(memberBalanceDTO);
        }
        // GET: MemberBalances/5
        [HttpGet("balance")]
        public async Task<ActionResult> GetBalanceAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser.UserType != Enums.UserType.Member)
                return BadRequest("User is not a member. No balance for this user");

            var member = await _memberService.GetMemberByUserId(currentUser.Id);
            if (member == null)
                return NotFound("Member does not exist");
            var memberBalance = await _memberBalanceService.GetAllMemberBalances(member.Id);

            return Ok(new ServiceResponse<BalancesDTO>()
            {
                Message = "Operation successufl",
                Data = memberBalance,
                Success = true
            });
        }

        // GET: MemberBalances/5
        [HttpGet("Item/{itemId}")]
        public async Task<ActionResult> GetByItemIdAsync(int itemId)
        {
            var memberBalance = await _memberBalanceService.GetMemberBalanceByItemId(itemId);
            var memberBalanceDTO = _mapper.Map<MemberBalanceDTO>(memberBalance);
            return Ok(memberBalanceDTO);
        }

        // GET: /MemberBalances/CronUpdateBalances
        [HttpGet("CronUpdateBalances")]
        public async Task<ActionResult> CronUpdateBalancesAsync()
        {
            ServiceResponse<string> serviceResponse = new ServiceResponse<string>();
            serviceResponse.Errors = new List<Error>();
            var transactions = await _savingDepositTransactionService.GetUnreflectedTransactions();
            int count = 0, failed = 0, pending;
            foreach (var item in transactions)
            {
                try
                {
                    if (item.DueDate.HasValue && item.DueDate.Value.ToShortDateString() == DateTime.UtcNow.ToShortDateString())
                    {
                        // Get member saving
                        List<MemberSaving> memberSavings = await _memberSavingService.GetSavingsByMemberIdAndType(item.MemberId, item.SavingsType);
                        var selectMemberSaving = memberSavings.Where(x => x.Type == item.SavingsType).FirstOrDefault();

                        //For Cash Addition
                        var memberBalance = await _memberBalanceService.GetMemberBalanceByItemId(selectMemberSaving.Id);
                        memberBalance.CurrentBalance += item.DepositAmount;
                        _memberBalanceService.UpdateMemberBalance(memberBalance);

                        // Update Savings Deposit transaction
                        item.HasReflected = true;
                        await _savingDepositTransactionService.SaveSavingDepositTransaction(item);

                        // Increase success count
                        count++;
                    }
                }
                catch (Exception ex)
                {
                    serviceResponse.Errors.Add(new Error { ErrorCode = "500", ErrorMessage = ex.Message });
                    failed++;
                    continue;
                }
                pending = transactions.Count - count + failed;
                serviceResponse.Data = $"{count}/{transactions.Count} balance(s) updated successfully, {failed} failed and {pending} pending.";
                serviceResponse.Message = "Cron job complete";
                serviceResponse.Success = true;
            }
            return Ok(serviceResponse);
        }
    }
}
