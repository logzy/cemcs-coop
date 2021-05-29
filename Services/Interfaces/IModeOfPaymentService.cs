using COOP.Banking.BusinessEntities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace COOP.Banking.Services.Interfaces
{
    public interface IModeOfPaymentService
    {
        Task<List<ModeOfPayment>> GetModesOfPayment();
    }
}
