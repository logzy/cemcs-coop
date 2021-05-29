using System;

namespace COOP.Banking.Data.DTO
{
    public class RepaymentDTO
    {
        public int Id { get; set; }
        public int LoanApplicationId { get; set; }
        public DateTime RepaymentDate { get; set; }
        public decimal Principal { get; set; }
        public decimal Interest { get; set; }
        public decimal TotalPayment { get; set; }
    }
}
