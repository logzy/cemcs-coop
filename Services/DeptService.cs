using COOP.Banking.BusinessEntities;
using COOP.Banking.Data;
using COOP.Banking.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COOP.Banking.Services
{
    public class DeptService : IDeptService
    {
        private readonly CoopBankingDataContext _context;
        public DeptService(CoopBankingDataContext context)
        {
            _context = context;
        }

        public async Task<Department> GetDeptById(int deptId)
        {
            var dept = await _context.Departments
                .Where(p => p.Id == deptId)
                .FirstOrDefaultAsync();
            return dept;
        }

        public async Task<List<Department>> GetDepts()
        {
            var dept = await _context.Departments
                .ToListAsync();
            return dept;
        }

        public async Task<Department> SaveDept(Department dept)
        {
            _context.Departments.Add(dept);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dept;
        }
        public async Task<Department> UpdateDept(Department dept)
        {
            _context.Attach(dept).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dept;
        }
    }
}