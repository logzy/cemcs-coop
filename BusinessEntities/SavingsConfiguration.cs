using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COOP.Banking.BusinessEntities
{
    public class SavingsConfiguration
    {
        public int Id { get; set; }
        public int MemberTypeId { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(10,2)")]
        public decimal MinimumSavingsAmount { get; set; }

        public MemberType MemberType { get; set; }
    }
}
