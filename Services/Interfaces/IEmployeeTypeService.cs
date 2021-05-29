using COOP.Banking.BusinessEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COOP.Banking.Services.Interfaces
{
    public interface IEmployeeTypeService
    {
        public Task<List<EmployeeType>> GetEmpTypes();
        public Task<EmployeeType> GetEmpType(int EmpTypeId);
        public Task<EmployeeType> SaveEmpType(EmployeeType EmpType);
        public Task<EmployeeType> UpdateEmpType(EmployeeType EmpType);
    }
}