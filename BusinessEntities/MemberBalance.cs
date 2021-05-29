using COOP.Banking.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COOP.Banking.BusinessEntities
{
    public class MemberBalance
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public Enums.BalanceType BalanceType { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "decimal(10,2)")]
        public decimal CurrentBalance { get; set; }
        public int ItemId { get; set; }
        public Member Member { get; set; }
    }
}
