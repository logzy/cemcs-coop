namespace COOP.Banking.BusinessEntities
{
    public class CreditCommittee
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int PositionId { get; set; }
        public Member Member { get; set; }
        public Position Position { get; set; }
    }
}
