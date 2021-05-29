namespace COOP.Banking.Data.DTO
{
    public class LoanGuarantorDTO
    {
        public int Id { get; set; }
        public string EmployeeNumber { get; set; }
        public string GuarantorName { get; set; }
        public string GuarantorEmail { get; set; }

        public int LoanApplicationId { get; set; }
        public LoanApplicationDTO LoanApplication { get; set; }

    }
}
