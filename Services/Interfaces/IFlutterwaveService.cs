using COOP.Banking.Data.DTO;
using System.Threading.Tasks;

namespace COOP.Banking.Services.Interfaces
{
    public interface IFlutterwaveService
    {
        Task<string> GetAccountName(AccountEnquiryDTO account);
    }
}
