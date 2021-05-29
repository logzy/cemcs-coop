using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COOP.Banking.BusinessEntities
{
    public class LoanOffsetApplication
    {
        public int Id { get; set; }
        public int LoanApplicationId { get; set; }
        public int MemberId { get; set; }

        public int LoanOffsetModeOfPaymentId { get; set; }
        public int FirstMonthOfDeduction { get; set; }
        public int FirstYearOfDeduction { get; set; }

        public int OffsetMonth { get; set; }
        public int OffsetYear { get; set; }
        public decimal OffsetAmount { get; set; }
        public int ModeOfPaymentId { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "decimal(10,2)")]
        public decimal DepositAmount { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "decimal(10,2)")]
        public decimal TransferAmount { get; set; }
    }
}
