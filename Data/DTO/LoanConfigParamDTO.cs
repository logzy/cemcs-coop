using COOP.Banking.BusinessEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COOP.Banking.Data.DTO
{
    public class LoanConfigParamDTO
    {
        [DataType(DataType.Currency), Column(TypeName = "decimal(10,2)")]
        public decimal MinLoanAmount { get; set; }
        public decimal MaxLoanAmount { get; set; }
        public int MonthlyRepayPeriod { get; set; }
        public int MinMonthlyRepayPeriod { get; set; }
        public int MaxMonthlyRepayPeriod { get; set; }
        public float IntrestRate { get; set; }
        public decimal LumpSumSavingsAmount { get; set; }
        public bool IsATargetLoan { get; set; }
        public decimal MonthlySavingsAmount { get; set; }
        public decimal ExistingLoanFeeAmount { get; set; }
        public float WaitingPeriod { get; set; }
        public int PeriodBeforeOffset { get; set; }
        public bool AllowPartialOffset { get; set; }
        public Enums.AdminChargeType AdminChargeType { get; set; }
        public float AdminChargeAmount { get; set; }
        public int LoanId { get; set; }
        public int MemberTypeId { get; set; }
        public bool AllowConcurent { get; set; }
        public int ConcurrentQualifyingPeriods { get; set; }

        public Loan Loan { get; set; }
        public string ErrorMessage { get; set; }
    }
}
