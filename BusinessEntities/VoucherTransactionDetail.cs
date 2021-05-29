using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COOP.Banking.BusinessEntities
{
    public class VoucherTransactionDetail
    {
        public int Id { get; set; }
        public int VoucherTransactionId { get; set; }
        public int AccountId { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }
        public string Narration { get; set; }
    }
}
