namespace COOP.Banking.Data.DTO
{
    public class LoanDTO
    {
        public int? Id { get; set; }
        public string LoanCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public Enums.LoanType LoanType { get; set; }
    }
}