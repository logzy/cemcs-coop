using COOP.Banking.BusinessEntities;
using COOP.Banking.Data;
using COOP.Banking.Data.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COOP.Banking.Services.Interfaces
{
    public interface ISavingsConfigurationService
    {
        public Task<ServiceResponse<List<SavingsConfigurationDTO>>> GetMinSavings();
        public Task<SavingsConfigurationDTO> GetMinSavingById(int savingsId);
        public Task<ServiceResponse<SavingsConfiguration>> CreateSavings(SavingsConfigurationDTO newSavings);
    }
}