using COOP.Banking.BusinessEntities;
using COOP.Banking.Data;
using COOP.Banking.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COOP.Banking.Services.Interfaces
{
    public interface IEmployeeService
    {
        public Task<ServiceResponse<List<EmployeeGetDTO>>> GetAllEmployee();
        public Task<List<EmployeeGetDTO>> GetInactiveEmployee();

        public Task<List<EmployeeGetDTO>> GetAllActiveEmployee();

        public Task<List<EmployeeGetDTO>> GetDeletedEmployee();
        public Task<EmployeeGetDTO> GetEmployeeByUserId(string employeeId);
        public Task<List<EmployeeGetDTO>> GetEmployeeByDept(int dept);
        public Task<Employee> SaveEmployee(Employee newEmp);
        public Task<ServiceResponse<EmployeeGetDTO>> UpdateEmployee(EmployeeRegDTO newEmp);

    }
}