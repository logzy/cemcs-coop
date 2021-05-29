namespace COOP.Banking.Data.DTO
{
    public class LoanPlannerParam
    {
        public int loanId { get; set; }
        public decimal loanAmount { get; set; }
        public decimal ExpectedAmount { get; set; }
        public int repaymentPeriod { get; set; }
    }
}
