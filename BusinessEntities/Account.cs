namespace COOP.Banking.BusinessEntities
{
    public class Account
    {
        public int Id { get; set; }
        public int AccountTypeId { get; set; }
        public string Code { get; set; }
        public string SageCode { get; set; }

        public AccountType AccountType { get; set; }
    }
}
