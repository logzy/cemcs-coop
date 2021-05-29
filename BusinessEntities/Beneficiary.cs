namespace COOP.Banking.BusinessEntities
{
    public class Beneficiary
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int BankId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public bool Primary { get; set; }

        public Member Member { get; set; }
        public Bank Bank { get; set; }
    }
}
