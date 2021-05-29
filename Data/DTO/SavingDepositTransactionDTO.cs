using COOP.Banking.BusinessEntities;
using System;

namespace COOP.Banking.Data.DTO
{
    public class SavingDepositTransactionDTO
    {
        public int? Id { get; set; }
        public int MemberId { get; set; }
        public int SavingsType { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal DepositAmount { get; set; }
        public int ApprovalCount { get; set; }
        public int Status { get; set; }
        public int TransactionTypeId { get; set; }
        public DateTime LastApprovalDate { get; set; }
        public string LastApprovedBy { get; set; }
        public bool? HasReflected { get; set; }
        public DateTime? DueDate { get; set; }
        public TransactionType TransactionType { get; set; }
        public Member Member { get; set; }
    }
}
