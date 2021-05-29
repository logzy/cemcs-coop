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
    public class DepartmentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IDeptService _deptService;
        private readonly IMapper _mapper;

        public DepartmentController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IDeptService deptService, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _deptService = deptService;
            _mapper = mapper;

        }

        // GET: /Department/All
        [HttpGet("All")]
        public async Task<ActionResult> GetAsync()
        {
            var dept = await _deptService.GetDepts();
            var deptDTO = _mapper.Map<List<DeptDTO>>(dept);
            return Ok(deptDTO);
        }

        // GET: Department/5
        [HttpGet("{deptId}")]
        public async Task<ActionResult> GetAsync(int deptId)
        {
            var dept = await _deptService.GetDeptById(deptId);
            var deptDTO = _mapper.Map<DeptDTO>(dept);
            return Ok(deptDTO);
        }

        // POST: /Department
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] DeptDTO deptDTO)
        {
            if (deptDTO.Id == null)
            {
                var dept = _mapper.Map<Department>(deptDTO);
                await _deptService.SaveDept(dept);
            }
            else
            {
                var mDept = await _deptService.GetDeptById((int)deptDTO.Id);
                if (mDept == null)
                    return NotFound("Department not found");

                mDept.Description = deptDTO.Description;
                await _deptService.UpdateDept(mDept);
            }

            return Ok(deptDTO);
        }


    }
}