using COOP.Banking.BusinessEntities;
using COOP.Banking.Data.DTO;
using COOP.Banking.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace COOP.Banking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class LoanController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILoanService _loanService;
        private readonly IMemberService _memberService;
        public LoanController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, ILoanService loanService, IMemberService memberService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _loanService = loanService;
            _memberService = memberService;
        }

        [HttpPost]
        public async Task<IActionResult> AddLoan([FromBody] LoanDTO newloan)
        {
            return Ok(await _loanService.CreateLoan(newloan));
        }

        // GET: Loans/apply
        [HttpPost("apply")]
        public async Task<IActionResult> Apply([FromBody] LoanPostDTO applicationData)
        {
            return Ok(await _loanService.SaveLoanApplication(applicationData));
        }

        // Post: Loans/submit
        [HttpPost("submit")]
        public async Task<IActionResult> SubmitLoan([FromForm] MimeLoanPostDTO applicationData)
        {
            return Ok(await _loanService.SaveMimeLoanApplication(applicationData));
        }
        // GET: Loans/All
        [HttpGet("All")]
        public async Task<IActionResult> GetAllLoan()
        {
            return Ok(await _loanService.GetLoans());
        }

        // GET: Loans/Paid
        [HttpGet("Paid")]
        public async Task<IActionResult> GetPaidLoans()
        {
            return Ok(await _loanService.GetPaidLoanApplications());
        }
        // GET: Loans/Unpaid
        [HttpGet("Unpaid")]
        public async Task<IActionResult> GetUnpaidLoans()
        {
            return Ok(await _loanService.GetUnpaidLoanApplications());
        }
        // GET: Loans/{loadId}
        [HttpGet("{loadId}")]
        public async Task<IActionResult> GetLoanId(int loadId)
        {
            return Ok(await _loanService.GetLoanById(loadId));
        }
        // GET: Loans/member/{memberId}
        [HttpGet("{memberId}")]
        public async Task<IActionResult> GetLoanByMemberId(int memberId)
        {
            return Ok(await _loanService.GetLoansByMemberId(memberId));
        }
        // GET: Loans/employeenumber
        [HttpGet("employeenumber")]
        public async Task<IActionResult> GetLoanByMemberEmployeeNumber()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            return Ok(await _loanService.GetLoansByEmployeeNumber(currentUser.UserName));
        }

        // GET: Loans/application/3
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetLoanApplicationById(int Id)
        {
            return Ok(await _loanService.GetLoanById(Id));
        }
        // GET: Loans/guarantor
        [HttpGet("api/Loans/guarantor")]
        public async Task<IActionResult> GetGuarantorsByMember()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var member = await _memberService.GetMemberByUserId(currentUser.Id);
            return Ok(await _loanService.GetGuarantors(member.Id));
        }

    }
}