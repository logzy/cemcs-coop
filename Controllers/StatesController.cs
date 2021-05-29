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
    public class StatesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IStateService _stateService;
        private readonly IMapper _mapper;
        public StatesController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IStateService stateService,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _stateService = stateService;
            _mapper = mapper;
        }

        // GET: /States/All
        [HttpGet("All")]
        public async Task<ActionResult> GetAsync()
        {
            var states = await _stateService.GetStates();
            var statesDTO = _mapper.Map<List<StateDTO>>(states);
            return Ok(statesDTO);
        }

        // GET: States/Details/5
        [HttpGet("{stateId}")]
        public async Task<ActionResult> GetAsync(int stateId)
        {
            var state = await _stateService.GetState(stateId);
            var stateDTO = _mapper.Map<StateDTO>(state);
            return Ok(stateDTO);
        }

        // POST: /States
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] StateDTO stateDTO)
        {
            if (stateDTO.Id == null)
            {
                var state = _mapper.Map<State>(stateDTO);
                await _stateService.SaveState(state);
            }
            else
            {
                var existingState = await _stateService.GetState((int)stateDTO.Id);
                if (existingState == null)
                    return NotFound("State not found");

                existingState.Name = stateDTO.Name;
                await _stateService.UpdateState(existingState);
            }
            return Ok(stateDTO);
        }

    }
}
