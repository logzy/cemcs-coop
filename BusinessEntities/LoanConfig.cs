using COOP.Banking.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COOP.Banking.BusinessEntities
{
    public class LoanConfig
    {
        public int Id { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(18,2)")]
        public decimal MinLoanAmount { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(18,2)")]
        public decimal MaxLoanAmount { get; set; }
        public int MonthlyRepayPeriod { get; set; }
        public int MinMonthlyRepayPeriod { get; set; }
        public int MaxMonthlyRepayPeriod { get; set; }
        public float IntrestRate { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(18,2)")]
        public decimal LumpSumSavingsAmount { get; set; }
        public bool IsATargetLoan { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(18,2)")]
        public decimal MonthlySavingsAmount { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(18,2)")]
        public decimal ExistingLoanFeeAmount { get; set; }
        public float WaitingPeriod { get; set; }
        public int PeriodBeforeOffset { get; set; }
        public bool AllowPartialOffset { get; set; }
        public Enums.AdminChargeType AdminChargeType { get; set; }
        public float AdminChargeAmount { get; set; }
        public int LoanId { get; set; }
        public int MemberTypeId { get; set; }
        public bool AllowConcurent { get; set; }
        public bool RequiresGuarantors { get; set; }
        public bool UseYearsOfService { get; set; }
        public int ServiceYearDuration { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(10,2)")]
        public decimal GuarantorMaximumAmount { get; set; }
        public int GuarantorCount { get; set; }
        public bool AllowTopUp { get; set; }
        public bool AllowTenureAdjustment { get; set; }
        public int ConcurrentQualifyingPeriods { get; set; }
        public int ConcurrentLoanCount { get; set; }
        public Loan Loan { get; set; }
        public MemberType MemberType { get; set; }

    }
}
