namespace COOP.Banking.BusinessEntities
{
    public class LoanOffsetModeOfPayment
    {
        public int Id { get; set; }
        public int LoanId { get; set; }
        public int ModeOfPaymentId { get; set; }

        public Loan Loan { get; set; }
        public ModeOfPayment ModeOfPayment { get; set; }
    }
}
