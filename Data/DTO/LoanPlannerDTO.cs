namespace COOP.Banking.Data.DTO
{
    public class LoanPlannerDTO
    {
        public decimal loanAmount { get; set; }
        public int maxRepaymentPeriod { get; set; }

        public float interestRate { get; set; }

        public decimal availableForLoan { get; set; }

        public decimal RequiredSavings { get; set; }
        public int oneThirdRepaymentPeriod { get; set; }

        public decimal minimumSavings { get; set; }

        public decimal actualMonthlySavings { get; set; }
        public decimal principal { get; set; }
        public decimal interest { get; set; }
        public decimal totalMonthly { get; set; }
    }
}
