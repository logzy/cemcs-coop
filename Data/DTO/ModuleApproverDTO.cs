using COOP.Banking.BusinessEntities;
using System.Collections.Generic;

namespace COOP.Banking.Data.DTO
{
    public class ModuleApproverDTO
    {
        public int? Id { get; set; }
        public int ModuleId { get; set; }
        public int Level { get; set; }

        public Module Module { get; set; }
        public ICollection<ModuleApproverNameStore> ModuleApproverNameStores { get; set; }
    }
}
