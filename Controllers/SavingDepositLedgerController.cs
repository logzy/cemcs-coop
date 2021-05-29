using AutoMapper;
using COOP.Banking.BusinessEntities;
using COOP.Banking.Data.DTO;
using COOP.Banking.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace COOP.Banking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public class SavingDepositLedgerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ISavingDepositLedgerService _savingDepositLedgerService;
        private readonly IMapper _mapper;

        public SavingDepositLedgerController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ISavingDepositLedgerService savingDepositLedgerService,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _savingDepositLedgerService = savingDepositLedgerService;
            _mapper = mapper;
        }

        // GET:SavingDepositLedgers/All
        [HttpGet("All")]
        public async Task<ActionResult> GetTaskAsync()
        {
            var savingDepositLedgers = await _savingDepositLedgerService.GetSavingDepositLedgers();
            var savingDepositLedgersDTO = _mapper.Map<List<SavingDepositLedgerDTO>>(savingDepositLedgers);
            return Ok(savingDepositLedgersDTO);

        }

        // GET: /SavingDepositLedgers/1
        [HttpGet("{savingDepositLedgerId}")]
        public async Task<ActionResult> GetAsync(int savingDepositLedgerId)
        {
            var savingDepositLedger = await _savingDepositLedgerService.GetSavingDepositLedger(savingDepositLedgerId);
            var savingDepositLedgerDTO = _mapper.Map<SavingDepositLedgerDTO>(savingDepositLedger);
            return Ok(savingDepositLedgerDTO);
        }

        // GET: /SavingDepositLedgers/Member/1
        [HttpGet("Member/{memberId}")]
        public async Task<ActionResult> GetLedgerByMemId(int memberId)
        {
            var savingDepositLedger = await _savingDepositLedgerService.GetLedgerByMemId(memberId);
            var savingDepositLedgerDTO = _mapper.Map<SavingDepositLedgerDTO>(savingDepositLedger);
            return Ok(savingDepositLedgerDTO);
        }

        // GET: /SavingDepositLedgers/MemberTrxSavings/1/3/2
        [HttpGet("MemberTrxSavings/{memberId}/{trxId}/{savingsType}")]
        public async Task<ActionResult> GetLedgerByMemIdTrxAndSavingsTypeAsync(int memberId, int trxId, int savingsType)
        {
            var savingDepositLedger = await _savingDepositLedgerService.GetLedgerByMemIdTrxAndSavingsType(memberId, trxId, savingsType);
            var savingDepositLedgerDTO = _mapper.Map<SavingDepositLedgerDTO>(savingDepositLedger);
            return Ok(savingDepositLedgerDTO);
        }

    }
}
