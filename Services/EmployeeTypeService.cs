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
    public class EmployeeTypeService : IEmployeeTypeService
    {
        private readonly CoopBankingDataContext _context;
        public EmployeeTypeService(CoopBankingDataContext context)
        {
            _context = context;
        }
        public async Task<EmployeeType> GetEmpType(int EmpTypeId)
        {
            var type = await _context.EmployeeTypes
               .Where(p => p.Id == EmpTypeId)
               .FirstOrDefaultAsync();
            return type;
        }

        public async Task<List<EmployeeType>> GetEmpTypes()
        {
            var type = await _context.EmployeeTypes
                .ToListAsync();
            return type;
        }

        public async Task<EmployeeType> SaveEmpType(EmployeeType EmpType)
        {
            _context.EmployeeTypes.Add(EmpType);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return EmpType;
        }

        public async Task<EmployeeType> UpdateEmpType(EmployeeType EmpType)
        {
            _context.Attach(EmpType).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return EmpType;
        }
    }
}