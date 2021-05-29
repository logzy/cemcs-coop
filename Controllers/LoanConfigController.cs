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
using System.Threading.Tasks;

namespace COOP.Banking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class LoanConfigController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILoanConfigService _loanConfigService;
        private readonly IMapper _mapper;
        private readonly IMemberService _memberService;
        public LoanConfigController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILoanConfigService loanConfigService,
            IMapper mapper,
             IMemberService memberService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _loanConfigService = loanConfigService;
            _mapper = mapper;
            _memberService = memberService;

        }


        // GET: LoanCongfig/All
        [HttpGet("All")]
        public async Task<ActionResult> GetAsync()
        {
            try
            {
                var loanConfig = await _loanConfigService.GetLoanConfigs();

                var ConfigDTO = _mapper.Map<List<LoanConfigDTO>>(loanConfig);

                return Ok(ConfigDTO);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] LoanConfigDTO LConfigDTO)
        {
            //var currentUser = await _userManager.GetUserAsync(User);
            var Config = _mapper.Map<LoanConfig>(LConfigDTO);
            await _loanConfigService.SaveLoanConfig(Config);

            return Ok(LConfigDTO);
        }
        //GET: api/LoanConfig/planner
        [HttpPost("/planner")]
        public async Task<ActionResult> GetLoanTypeDetails([FromBody] LoanPlannerParam param)
        {
            ServiceResponse<LoanPlannerDTO> response = new ServiceResponse<LoanPlannerDTO>();
            var currentUser = await _userManager.GetUserAsync(User);
            var memType = (int)currentUser.UserTypeCategory;
            var member = await _memberService.GetMemberByUserId(currentUser.Id);
            var loan = await _loanConfigService.GetLoanPlanner(param, memType, member.Id);

            response.Data = loan;
            response.Success = true;
            response.Message = "Operation Successful";


            return Ok(response);
        }

        //GET: "loantype/amount/{LoanId}"
        [HttpGet("{LoanId}")]

        public async Task<ActionResult> GetLoanTypeDetails(int LoanId)
        {
            ServiceResponse<LoanConfig> response = new ServiceResponse<LoanConfig>();
            var currentUser = await _userManager.GetUserAsync(User);
            var memType = (int)currentUser.UserTypeCategory;
            var member = await _memberService.GetMemberByUserId(currentUser.Id);
            //var loan = await _loanConfigService.GetLoanConfigByLoanIdAndMember(LoanId, memType);


            //var configDTO = _mapper.Map<LoanConfigDTO>(loan);
            var configDTO = await _loanConfigService.GetLoanParmsByLoanIdAndMember(LoanId, memType, member.Id);
            return Ok(configDTO);
        }

        //GET: LoanCongfig/Member/Type
        [HttpGet("Type")]
        public async Task<ActionResult> GetLoansByMemTypeAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var memType = (int)currentUser.UserTypeCategory;
            try
            {
                var loanConfig = await _loanConfigService.GetLoanConfigsByMemType(memType);

                var ConfigDTO = _mapper.Map<List<LoanConfigDTO>>(loanConfig);

                return Ok(ConfigDTO);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        // GET: "LoanConfig/Guarantors/count"
        [HttpPost("count")]
        public async Task<ActionResult> GetGuarantorCount(GuarantorParamsDTO param)
        {
            ServiceResponse<int> response = new ServiceResponse<int>();
            response.Success = true;
            response.Message = "Operation Successful";

            var config = await _loanConfigService.GetGuarantorsCount(param);
            response.Data = config;
            return Ok(response);
        }

    }
}