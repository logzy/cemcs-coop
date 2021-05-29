using COOP.Banking.BusinessEntities;

namespace COOP.Banking.Data.DTO
{
    public class MemberSavingDTO
    {
        public int? Id { get; set; }
        public int MemberId { get; set; }
        public decimal SavingsAmount { get; set; }
        public int Type { get; set; }
        public Member Member { get; set; }

    }
}