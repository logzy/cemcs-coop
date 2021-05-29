namespace COOP.Banking.BusinessEntities
{
    public class LoanBeneficiary
    {
        public int Id { get; set; }
        public int LoanApplicationId { get; set; }
        public int BeneficiaryId { get; set; }

        //public LoanApplication LoanApplication { get; set; }
        public Beneficiary Beneficiary { get; set; }
    }
}
