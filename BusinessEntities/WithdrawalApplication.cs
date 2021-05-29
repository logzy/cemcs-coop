using COOP.Banking.Data;
using System;

namespace COOP.Banking.BusinessEntities
{
    public class WithdrawalApplication
    {
        public int Id { get; set; }
        public int MemberId { get; set; }

        public Enums.SavingsType SourceSavingsType { get; set; }

        public DateTime TransactionDate { get; set; }
        public DateTime EffectiveDate { get; set; }

        public decimal Amount { get; set; }

        public int MethodOfCollectionId { get; set; }

        public int ApprovalCount { get; set; }

        public int BankId { get; set; }

        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string CollectionLocation { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public DateTime? LastApprovalDate { get; set; }
        public string CreatedBy { get; set; }
        public Member Member { get; set; }

        public MethodOfCollection MethodOfCollection { get; set; }
        public Bank Bank { get; set; }
    }
}
