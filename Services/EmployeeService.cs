using AutoMapper;
using COOP.Banking.BusinessEntities;
using COOP.Banking.Data;
using COOP.Banking.Data.DTO;
using COOP.Banking.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COOP.Banking.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly CoopBankingDataContext _context;
        private IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public EmployeeService(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            CoopBankingDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<ServiceResponse<List<EmployeeGetDTO>>> GetAllEmployee()
        {
            ServiceResponse<List<EmployeeGetDTO>> Response = new ServiceResponse<List<EmployeeGetDTO>>();

            List<Employee> employee = await _context.Employees
                                        .Include(p => p.Person)
                                        .Include(d => d.Department)
                                        .Include(e => e.EmployeeType).ToListAsync();

            Response.Data = (employee.Select(c => _mapper.Map<EmployeeGetDTO>(c))).ToList();
            return Response;
        }

        public async Task<List<EmployeeGetDTO>> GetDeletedEmployee()
        {
            var employee = await _context.Employees
                            .Where(e => e.Deleted).ToListAsync();
            return employee.Select(c => _mapper.Map<EmployeeGetDTO>(c)).ToList();
        }

        public async Task<EmployeeGetDTO> GetEmployeeByUserId(string employeeId)
        {
            Employee employee = await _context.Employees
                                        .Include(p => p.Person)
                                        .Include(d => d.Department)
                                        .Include(e => e.EmployeeType)
                                        .FirstOrDefaultAsync(c => c.UserId == employeeId);

            return _mapper.Map<EmployeeGetDTO>(employee);
        }

        public async Task<List<EmployeeGetDTO>> GetInactiveEmployee()
        {
            var employee = await _context.Employees
                            .Where(e => !e.Active).ToListAsync();
            return employee.Select(c => _mapper.Map<EmployeeGetDTO>(c)).ToList();
        }

        public async Task<List<EmployeeGetDTO>> GetAllActiveEmployee()
        {
            var employee = await _context.Employees
                            .Where(e => e.Active).ToListAsync();
            return employee.Select(c => _mapper.Map<EmployeeGetDTO>(c)).ToList();
        }

        public async Task<Employee> SaveEmployee(Employee newEmp)
        {
            _context.Employees.Add(newEmp);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newEmp;
        }

        public async Task<ServiceResponse<EmployeeGetDTO>> UpdateEmployee(EmployeeRegDTO newEmp)
        {
            ServiceResponse<EmployeeGetDTO> response = new ServiceResponse<EmployeeGetDTO>();

            try
            {
                Employee employee = await _context.Employees
                                .FirstOrDefaultAsync(c => c.Id == newEmp.Id);

                employee.JobTitle = newEmp.JobTitle;
                employee.DateOfHire = newEmp.DateOfHire;
                employee.ResignationDate = newEmp.ResignationDate;
                employee.AnnualSalary = newEmp.AnnualSalary;
                employee.BasicSalary = newEmp.BasicSalary;
                employee.Active = newEmp.Active;
                employee.Deleted = newEmp.Deleted;
                employee.DateCreated = newEmp.DateCreated;
                employee.LastModifiedDate = DateTime.UtcNow;
                employee.LastModifiedBy = "user";
                employee.DepartmentId = newEmp.DepartmentId;
                employee.EmployeeTypeId = newEmp.EmployeeTypeId;

                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<EmployeeGetDTO>(employee);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<List<EmployeeGetDTO>> GetEmployeeByDept(int dept)
        {
            List<Employee> employee = await _context.Employees
                                        .Include(p => p.Person)
                                        .Include(d => d.Department)
                                        .Where(c => c.DepartmentId == dept).ToListAsync();
            return employee.Select(c => _mapper.Map<EmployeeGetDTO>(c)).ToList();

        }
    }
}