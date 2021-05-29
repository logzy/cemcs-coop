using System;

namespace COOP.Banking.Data.DTO
{
    [Serializable]
    public class BalancesDTO
    {
        public int MemberId { get; set; }
        public decimal SpecialDepositBalance { get; set; }
        public decimal SavingsBalance { get; set; }
        public decimal TotalLoanBalance { get; set; }
        public string SpecifiedLoanDescription { get; set; }
        public decimal SpecifiedLoanBalance { get; set; }
        public decimal MonthlyDeductions { get; set; }

        public decimal MonthlyDepositDeductions { get; set; }
    }
}
