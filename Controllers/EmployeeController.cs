using AutoMapper;
using COOP.Banking.BusinessEntities;
using COOP.Banking.Data;
using COOP.Banking.Data.DTO;
using COOP.Banking.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace COOP.Banking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EmployeeController(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IEmployeeService employeeService,
            IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _employeeService = employeeService;
            _mapper = mapper;
        }

        // POST: /employee
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeRegDTO newEmp)
        {
            ServiceResponse<EmployeeGetDTO> Response = new ServiceResponse<EmployeeGetDTO>();

            var currentUser = await _userManager.GetUserAsync(User);

            try
            {
                Employee employee = _mapper.Map<Employee>(newEmp);
                employee.DateCreated = DateTime.UtcNow;
                employee.ResignationDate = DateTime.UtcNow;
                employee.UserId = newEmp.UserId;
                employee.Active = false;
                employee.Deleted = false;
                employee.CreatedBy = "admin";
                employee.LastModifiedBy = "user";
                employee.Person.CreatedBy = "admin";
                employee.Person.LastModifiedBy = "user";
                await _employeeService.SaveEmployee(employee);
                return Ok(employee);


            }
            catch (Exception ex)
            {
                Response.Success = false;
                Response.Message = ex.Message;
            }
            return Ok(Response);
        }


        // POST: /employee
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            return Ok(await _employeeService.GetAllEmployee());
        }

        // [HttpGet("api/employee/{empId}")]
        // public async Task<IActionResult> GetEmployeeId(int empId)
        // {
        //     return Ok(await _employeeService.GetEmployeeById(empId));
        // }

        // GET: /employee/dept/{deptId}
        [HttpGet("{deptId}")]
        public async Task<IActionResult> GetEmployeeDept(int deptId)
        {
            return Ok(await _employeeService.GetEmployeeByDept(deptId));
        }

        // GET: /active
        [HttpGet]
        public async Task<IActionResult> GetActive()
        {
            return Ok(await _employeeService.GetAllActiveEmployee());
        }

        //GET: /delete
        [HttpGet]
        public async Task<IActionResult> GetDeleted()
        {
            return Ok(await _employeeService.GetDeletedEmployee());
        }

        //GET: /inactive
        [HttpGet]
        public async Task<IActionResult> GetInactive()
        {
            return Ok(await _employeeService.GetInactiveEmployee());
        }



        //PUT: update/employee
        [HttpPut]
        public async Task<IActionResult> Update(EmployeeRegDTO newEmp)
        {
            ServiceResponse<EmployeeGetDTO> response = await _employeeService.UpdateEmployee(newEmp);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }





    }
}