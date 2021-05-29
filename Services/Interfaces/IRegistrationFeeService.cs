using COOP.Banking.BusinessEntities;
using System.Threading.Tasks;

namespace COOP.Banking.Services.Interfaces
{
    public interface IRegistrationFeeService
    {
        public Task<RegistrationFee> GetRegFeeByMemId(int memId);
        public Task<RegistrationFee> SaveRegFee(RegistrationFee RegFee);
        public Task<RegistrationFee> UpdateRegFee(RegistrationFee RegFee);

    }
}