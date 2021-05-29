using COOP.Banking.BusinessEntities;
using COOP.Banking.Data;
using COOP.Banking.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COOP.Banking.Services.Interfaces
{
    public interface IBankService
    {
        Task<ServiceResponse<List<BankDTO>>> GetBanks();

        Task<Bank> GetBankByCode(string code);
    }
}
