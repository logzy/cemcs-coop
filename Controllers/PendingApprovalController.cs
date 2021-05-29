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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PendingApprovalController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IApproverService _approverService;
        private readonly IPendingApprovalService<ModuleApprover> _approvalService;
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;
        public PendingApprovalController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IApproverService approverService,
            IPendingApprovalService<ModuleApprover> approvalService,
            IPersonService personService,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _approverService = approverService;
            _approvalService = approvalService;
            _personService = personService;
            _mapper = mapper;
        }

        // GET: /Approvers/All
        [HttpGet("UserPending")]
        public async Task<ActionResult> GetUserPendingApprovalAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var userPending = await _approvalService.GetUserPendingApproval(currentUser.UserName);
            var pendingApprovalDTO = _mapper.Map<List<PendingApprovalDTO>>(userPending);

            //Response//
            ServiceResponse<List<PendingApprovalDTO>> response = new ServiceResponse<List<PendingApprovalDTO>>();
            response.Data = pendingApprovalDTO;
            response.Success = true;
            return Ok(response);
        }


        // GET: /PendingApproval/All
        [HttpGet("All")]
        public async Task<ActionResult> GetPendingApprovalsAsync()
        {

            var pendingApprovals = await _approvalService.GetPendingApprovals();
            var pendingApprovalsDTO = _mapper.Map<List<PendingApprovalDTO>>(pendingApprovals);

            //Response//
            ServiceResponse<List<PendingApprovalDTO>> response = new ServiceResponse<List<PendingApprovalDTO>>();
            response.Data = pendingApprovalsDTO;
            response.Success = true;
            return Ok(response);
        }

        // GET: /Approvers/PendingApprovals/2
        [HttpGet("{pendingApprovalId}")]
        public async Task<ActionResult> GetPendingApprovalAsync(int pendingApprovalId)
        {
            var pendingApproval = await _approvalService.GetPendingApproval(pendingApprovalId);
            var pendingApprovalDTO = _mapper.Map<PendingApprovalDTO>(pendingApproval);

            //Response//
            ServiceResponse<PendingApprovalDTO> response = new ServiceResponse<PendingApprovalDTO>();
            response.Data = pendingApprovalDTO;
            response.Success = true;
            return Ok(response);
        }

        // POST: /Approve/
        [HttpPost("Approve")]
        public async Task<ActionResult> ApproveAsync([FromBody] PendingApprovalDTO pendingApprovalDTO)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            ServiceResponse<PendingApproval> response = new ServiceResponse<PendingApproval>();
            if (pendingApprovalDTO.Id == 0)
            {
                response.Success = false;
                response.Message = "invalid Pending Approval id";
                return BadRequest(response);
            }
            else
            {
                var pendingApproval = await _approvalService.GetPendingApproval(pendingApprovalDTO.Id);
                if (pendingApproval.Approved == false)
                    return Ok(new ServiceResponse<PendingApproval> { Success = false, Message = $"{pendingApproval.ModuleApprover.Module.Name} with id \"{pendingApproval.ItemId}\" has already been approved" });

                response = pendingApproval.ModuleApprover.Module.NormalizedName switch
                {
                    "MEMBERSHIPS" => await _approvalService.ApproveMembershipModule(pendingApproval, currentUser.UserName, pendingApprovalDTO.Approved),
                    "LOANS" => await _approvalService.ApproveLoanModule(pendingApproval, currentUser.UserName, pendingApprovalDTO.Approved),
                    "SAVINGS" => await _approvalService.ApproveSavingsModule(pendingApproval, currentUser.UserName, pendingApprovalDTO.Approved),
                    "CASH-ADDITION" => await _approvalService.ApproveCashAdditionModule(pendingApproval, currentUser.UserName, pendingApprovalDTO.Approved),
                    "WAIVERS" => await _approvalService.ApproveWaiversModule(pendingApproval, currentUser.UserName, pendingApprovalDTO.Approved),
                    "LOAN-OFFSETS" => await _approvalService.ApproveLoanOffsetsModule(pendingApproval, currentUser.UserName, pendingApprovalDTO.Approved),
                    "TRANSFER" => await _approvalService.ApproveTransferModule(pendingApproval, currentUser.UserName, pendingApprovalDTO.Approved),
                    _ => new ServiceResponse<PendingApproval> { Success = false, Message = $"Invalid Module Name \"{pendingApproval.ModuleApprover.Module.Name}\"" },
                };
            }
            //Response//
            return Ok(response);
        }

        // GET: /PendingApproval/Members
        [HttpGet("Members")]
        public async Task<ActionResult> GetPendingMemberApprovalsAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var pendingApprovals = await _approvalService.GetUserPendingMemberApprovals(currentUser.UserName);

            //Response//
            ServiceResponse<List<PendingApprovalRequestDTO<Member>>> response = new ServiceResponse<List<PendingApprovalRequestDTO<Member>>>();
            response.Data = pendingApprovals;
            response.Success = true;
            return Ok(response);
        }

        // GET: /PendingApproval/Savings-Increase-Decrease
        [HttpGet("Savings-Increase-Decrease")]
        public async Task<ActionResult> GetPendingSavingApprovalsAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var pendingApprovals = await _approvalService.GetUserPendingSavingApprovals(currentUser.UserName);

            //Response//
            ServiceResponse<List<PendingApprovalRequestDTO<SavingDepositTransaction>>> response = new ServiceResponse<List<PendingApprovalRequestDTO<SavingDepositTransaction>>>();
            response.Data = pendingApprovals;
            response.Success = true;
            return Ok(response);
        }
        // GET: /PendingApproval/Cash-Addition
        [HttpGet("Cash-Addition")]
        public async Task<ActionResult> GetPendingCashApprovalsAsync()
        {

            var currentUser = await _userManager.GetUserAsync(User);

            var pendingApprovals = await _approvalService.GetUserPendingCashApprovals(currentUser.UserName);

            //Response//
            ServiceResponse<List<PendingApprovalRequestDTO<SavingDepositTransaction>>> response = new ServiceResponse<List<PendingApprovalRequestDTO<SavingDepositTransaction>>>();
            response.Data = pendingApprovals;
            response.Success = true;
            return Ok(response);
        }
        // GET: /PendingApproval/Loan
        [HttpGet("Loan")]
        public async Task<ActionResult> GetPendingLoansAsync()
        {

            var currentUser = await _userManager.GetUserAsync(User);

            var pendingApprovals = await _approvalService.GetUserPendingLoanApprovals(currentUser.UserName);

            //Response//
            ServiceResponse<List<PendingApprovalRequestDTO<LoanApplication>>> response = new ServiceResponse<List<PendingApprovalRequestDTO<LoanApplication>>>();
            response.Data = pendingApprovals;
            response.Success = true;
            return Ok(response);
        }

    }
}
