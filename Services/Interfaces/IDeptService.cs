using COOP.Banking.BusinessEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COOP.Banking.Services.Interfaces
{
    public interface IDeptService
    {
        public Task<List<Department>> GetDepts();
        public Task<Department> GetDeptById(int stateId);
        public Task<Department> SaveDept(Department dept);
        public Task<Department> UpdateDept(Department dept);
    }
}