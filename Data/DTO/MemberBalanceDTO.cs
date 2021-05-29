using static AutoMapper.Internal.ExpressionFactory;

namespace COOP.Banking.Data.DTO
{
    public class MemberBalanceDTO
    {
        public int? Id { get; set; }
        public int MemberId { get; set; }
        public Enums.BalanceType BalanceType { get; set; }
        public int ItemId { get; set; }
        public Member Member { get; set; }
    }
}
