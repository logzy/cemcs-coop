using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COOP.Banking.BusinessEntities
{
    public class MemberSaving
    {
        public int Id { get; set; }
        public int MemberId { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(10,2)")]
        public decimal SavingsAmount { get; set; }
        public int Type { get; set; }

        public Member Member { get; set; }
    }
}
