using System.Collections.Generic;

namespace COOP.Banking.BusinessEntities
{
    public class ModuleApprover
    {
        public int Id { get; set; }
        public int ModuleId { get; set; }
        public int Level { get; set; }

        public Module Module { get; set; }
        public ICollection<ModuleApproverNameStore> ModuleApproverNameStores { get; set; }
    }
}
