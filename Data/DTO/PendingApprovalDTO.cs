using COOP.Banking.BusinessEntities;
using System;

namespace COOP.Banking.Data.DTO
{
    [Serializable]
    public class PendingApprovalDTO
    {
        public int Id { get; set; }
        public int ModuleApproverId { get; set; }
        public int ItemId { get; set; }
        public bool? Approved { get; set; }

        public ModuleApprover ModuleApprover { get; set; }

    }
}
