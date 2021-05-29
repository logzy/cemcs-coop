using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COOP.Banking.BusinessEntities
{
    public class RegistrationFee
    {
        public int Id { get; set; }
        public int MemberTypeId { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }
    }
}
