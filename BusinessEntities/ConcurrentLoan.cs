namespace COOP.Banking.BusinessEntities
{
    public class ConcurrentLoan
    {
        public int Id { get; set; }
        public int LoanId { get; set; }

        public Loan Loan { get; set; }
        public int? ConcurrentLoanId { get; set; }
    }
}
