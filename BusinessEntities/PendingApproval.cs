namespace COOP.Banking.BusinessEntities
{
    public class PendingApproval
    {
        public int Id { get; set; }
        public int ModuleApproverId { get; set; }
        public int ItemId { get; set; }
        public bool? Approved { get; set; }
        public int CurrentLevel { get; set; }

        public ModuleApprover ModuleApprover { get; set; }

    }
}
