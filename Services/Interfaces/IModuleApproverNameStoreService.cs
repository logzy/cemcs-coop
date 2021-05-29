using COOP.Banking.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COOP.Banking.Services.Interfaces
{
    public interface IModuleApproverNameStoreService
    {
        Task<List<ApproverDetailDTO>> GetApproverDetailsByLevel(int Module, int Level);
    }
}
