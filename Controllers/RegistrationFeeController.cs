using AutoMapper;
using COOP.Banking.BusinessEntities;
using COOP.Banking.Data.DTO;
using COOP.Banking.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace COOP.Banking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public class RegistrationFeeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRegistrationFeeService _registrationFeeService;
        private readonly IMapper _mapper;

        public RegistrationFeeController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IRegistrationFeeService registrationFeeService,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _registrationFeeService = registrationFeeService;
            _mapper = mapper;
        }

        // GET: memberReg/type/1
        [HttpGet("{MemId}")]
        public async Task<ActionResult> GetAsync(int MemId)
        {
            var regFee = await _registrationFeeService.GetRegFeeByMemId(MemId);
            var regFeeDTO = _mapper.Map<RegistrationFeeDTO>(regFee);
            return Ok(regFeeDTO);
        }

        // POST: /MemberRegistration
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] RegistrationFeeDTO regFeeDTO)
        {
            if (regFeeDTO.Id == null)
            {
                var regFee = _mapper.Map<RegistrationFee>(regFeeDTO);
                await _registrationFeeService.SaveRegFee(regFee);
            }
            return Ok(regFeeDTO);
        }



    }
}