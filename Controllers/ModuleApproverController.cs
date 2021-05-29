using AutoMapper;
using COOP.Banking.BusinessEntities;
using COOP.Banking.Data;
using COOP.Banking.Data.DTO;
using COOP.Banking.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COOP.Banking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = "Bearer")]
    public class ModuleApproversController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IModuleApproverService _moduleApproverService;
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;
        public ModuleApproversController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IModuleApproverService moduleApproverService,
            IPersonService personService,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _moduleApproverService = moduleApproverService;
            _personService = personService;
            _mapper = mapper;
        }

        // GET: /ModuleApprovers/All
        [HttpGet("All")]
        public async Task<ActionResult> GetAsync()
        {

            var moduleApprovers = await _moduleApproverService.GetModuleApprovers();
            var moduleApproversDTO = _mapper.Map<List<ModuleApproverDTO>>(moduleApprovers);

            //Response//
            ServiceResponse<List<ModuleApproverDTO>> response = new ServiceResponse<List<ModuleApproverDTO>>();
            response.Data = moduleApproversDTO;
            response.Success = true;
            return Ok(response);
        }

        // GET: /ModuleApprovers/Modules
        [HttpGet("Modules")]
        public async Task<ActionResult> GetModulesAsync()
        {

            var modules = await _moduleApproverService.GetModules();

            //Response//
            ServiceResponse<List<Module>> response = new ServiceResponse<List<Module>>();
            response.Data = modules;
            response.Success = true;
            return Ok(response);
        }
        // GET: /ModuleApprovers/ModulesWithoutApprovers
        [HttpGet("ModulesWithoutApprovers")]
        public async Task<ActionResult> GetModulesWithoutApproversAsync()
        {

            var modules = await _moduleApproverService.GetModulesWithoutApprovers();

            //Response//
            ServiceResponse<List<Module>> response = new ServiceResponse<List<Module>>();
            response.Data = modules;
            response.Success = true;
            return Ok(response);
        }

        // GET: ModuleApprovers/5
        [HttpGet("{moduleApproverId}")]
        public async Task<ActionResult> GetAsync(int moduleApproverId)
        {
            var moduleApprover = await _moduleApproverService.GetModuleApprover(moduleApproverId);
            var moduleApproverDTO = _mapper.Map<ModuleApproverDTO>(moduleApprover);

            //Response//
            ServiceResponse<ModuleApproverDTO> response = new ServiceResponse<ModuleApproverDTO>();
            response.Data = moduleApproverDTO;
            response.Success = true;
            return Ok(response);
        }

        // POST: /ModuleApprovers
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] ModuleApproverDTO moduleApproverDTO)
        {
            if (moduleApproverDTO.Id == null)
            {
                var moduleApprover = _mapper.Map<ModuleApprover>(moduleApproverDTO);
                if (moduleApproverDTO.ModuleApproverNameStores != null && moduleApproverDTO.Level != moduleApproverDTO.ModuleApproverNameStores.Count)
                    return BadRequest("Module Approver Level not equal to approver list levels");

                await _moduleApproverService.SaveModuleApprover(moduleApprover);
            }
            else
            {
                var existingModuleApprover = await _moduleApproverService.GetModuleApprover((int)moduleApproverDTO.Id);
                if (existingModuleApprover == null)
                    return NotFound("ModuleApprover not found");

                existingModuleApprover.ModuleId = moduleApproverDTO.ModuleId;
                existingModuleApprover.Level = moduleApproverDTO.Level;

                foreach (var item in moduleApproverDTO.ModuleApproverNameStores)
                {
                    if (existingModuleApprover.ModuleApproverNameStores.Any(e => e.ApprovalLevel == item.ApprovalLevel))
                    {
                        existingModuleApprover.ModuleApproverNameStores
                       .Where(m => m.ApprovalLevel == item.ApprovalLevel)
                       .First().Usernames = item.Usernames;
                    }
                    else
                    {
                        existingModuleApprover.ModuleApproverNameStores.Add(new ModuleApproverNameStore
                        {
                            Usernames = item.Usernames,
                            ApprovalLevel = item.ApprovalLevel
                        });
                    }
                }

                foreach (var item in existingModuleApprover.ModuleApproverNameStores)
                {
                    if (!moduleApproverDTO.ModuleApproverNameStores.Any(e => e.ApprovalLevel == item.ApprovalLevel))
                        _moduleApproverService.RemoveApproverNameStore(item);
                }

                await _moduleApproverService.UpdateModuleApprover(existingModuleApprover);
            }
            //Response//
            ServiceResponse<ModuleApproverDTO> response = new ServiceResponse<ModuleApproverDTO>
            {
                Data = moduleApproverDTO,
                Success = true,
                Message = "ModuleApprover saved"
            };
            return Ok(response);
        }
    }
}
