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
    public class ApproversController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IApproverService _approverService;
        private readonly IPendingApprovalService<ModuleApprover> _approvalService;
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;
        public ApproversController(UserManager<ApplicationUser> userManager,
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
        [HttpGet("All")]
        public async Task<ActionResult> GetAsync()
        {

            var approvers = await _approverService.GetApprovers();
            var approversDTO = _mapper.Map<List<ApproverDTO>>(approvers);

            //Response//
            ServiceResponse<List<ApproverDTO>> response = new ServiceResponse<List<ApproverDTO>>();
            response.Data = approversDTO;
            response.Success = true;
            return Ok(response);
        }

        // GET: Approvers/Details/5
        [HttpGet("{approverId}")]
        public async Task<ActionResult> GetAsync(int approverId)
        {
            var approver = await _approverService.GetApprover(approverId);
            var approverDTO = _mapper.Map<ApproverDTO>(approver);

            //Response//
            ServiceResponse<ApproverDTO> response = new ServiceResponse<ApproverDTO>();
            response.Data = approverDTO;
            response.Success = true;
            return Ok(response);
        }

        // POST: /Approvers
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] ApproverDTO approverDTO)
        {
            if (approverDTO.Id == null)
            {
                var approver = _mapper.Map<Approver>(approverDTO);
                if (!Enum.IsDefined(typeof(Enums.ApproverType), approverDTO.ApproverType))
                    return BadRequest("Approval Type does not exist");
                await _approverService.SaveApprover(approver);
            }
            else
            {
                var existingApprover = await _approverService.GetApprover((int)approverDTO.Id);
                if (existingApprover == null)
                    return NotFound("Approver not found");

                existingApprover.ApproverType = approverDTO.ApproverType;
                existingApprover.ApproverRoleId = approverDTO.ApproverRoleId;
                await _approverService.UpdateApprover(existingApprover);
            }
            //Response//
            ServiceResponse<ApproverDTO> response = new ServiceResponse<ApproverDTO>();
            response.Data = approverDTO;
            response.Success = true;
            response.Message = "Approver saved";
            return Ok(response);
        }
    }
}
