namespace COOP.Banking.BusinessEntities
{
    public class ModuleApproverNameStore
    {
        public int Id { get; set; }
        public int ModuleApproverId { get; set; }
        public string Usernames { get; set; }
        public int ApprovalLevel { get; set; }
        bool HasUsername(string user)
        {
            return Usernames.Contains(user);
        }
    }
}
