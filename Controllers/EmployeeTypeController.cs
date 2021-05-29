using AutoMapper;
using COOP.Banking.BusinessEntities;
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
    public class EmployeeTypeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmployeeTypeService _employeeTypeService;
        private readonly IMapper _mapper;

        public EmployeeTypeController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IEmployeeTypeService employeeTypeService, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _employeeTypeService = employeeTypeService;
            _mapper = mapper;

        }

        // GET: /EmpType/All
        [HttpGet("All")]
        public async Task<ActionResult> GetAsync()
        {
            var type = await _employeeTypeService.GetEmpTypes();
            var typeDTO = _mapper.Map<List<EmployeeTypeDTO>>(type);
            return Ok(typeDTO);
        }

        // GET: EmpType/5
        [HttpGet("{typeId}")]
        public async Task<ActionResult> GetAsync(int typeId)
        {
            var type = await _employeeTypeService.GetEmpType(typeId);
            var typeDTO = _mapper.Map<DeptDTO>(type);
            return Ok(typeDTO);
        }

        // POST: /EmpType
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] EmployeeTypeDTO emp)
        {
            if (emp.Id == null)
            {
                var type = _mapper.Map<EmployeeType>(emp);
                await _employeeTypeService.SaveEmpType(type);
            }
            else
            {
                var mType = await _employeeTypeService.GetEmpType((int)emp.Id);
                if (mType == null)
                    return NotFound("Employee Type not found");

                mType.Description = emp.Description;
                await _employeeTypeService.UpdateEmpType(mType);
            }

            return Ok(emp);
        }


    }
}