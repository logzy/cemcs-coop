using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COOP.Banking.BusinessEntities
{
    public class LoanRepayment
    {
        public int Id { get; set; }
        public int LoanApplicationId { get; set; }
        public DateTime RepaymentDate { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(10,2)")]
        public decimal Principal { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "decimal(10,2)")]
        public decimal Interest { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(10,2)")]
        public decimal Fees { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(10,2)")]
        public decimal TotalPayment { get; set; }
        public bool? Paid { get; set; }
        public DateTime? DatePaid { get; set; }

        public LoanApplication LoanApplication { get; set; }
        //public Member Member { get; set; }
    }
}
