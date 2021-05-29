using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COOP.Banking.BusinessEntities
{
    public class LoanGuarantorConfig
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public int GuarantorCount { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(18,2)")]
        public decimal GuarantorMinimumAmount { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(18,2)")]
        public decimal GuarantorMaximumAmount { get; set; }
        public Loan Loan { get; set; }
    }
}
