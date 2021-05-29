using COOP.Banking.BusinessEntities;
using System;

namespace COOP.Banking.Data.DTO
{
    public class SavingDepositLedgerDTO
    {
        public int? Id { get; set; }
        public int MemberId { get; set; }
        public int SavingsType { get; set; }

        public DateTime TransactionDate { get; set; }

        public decimal PreviousBalance { get; set; }

        public decimal DepositAmount { get; set; }

        public decimal CurrentBalance { get; set; }
        public int Status { get; set; }
        public int TransactionTypeId { get; set; }

        public TransactionType TransactionType { get; set; }
        public Member Member { get; set; }
    }
}
