using System;

namespace COOP.Banking.Data.DTO
{
    [Serializable]
    public class WithdrawalSaveDTO
    {
        public int MemberId { get; set; }

        public Enums.SavingsType DebitAccountType { get; set; }
        public int MethodOfCollectionId { get; set; }
        public string CollectionLocation { get; set; }
        public string BankCode { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public decimal Amount { get; set; }
    }
}
