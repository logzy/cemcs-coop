using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COOP.Banking.BusinessEntities
{
    public class SavingDepositTransaction
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int SavingsType { get; set; }
        public DateTime TransactionDate { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(10,2)")]
        public decimal DepositAmount { get; set; }
        public int ApprovalCount { get; set; }
        public int Status { get; set; }
        public int TransactionTypeId { get; set; }
        public DateTime LastApprovalDate { get; set; }
        public string LastApprovedBy { get; set; }

        public TransactionType TransactionType { get; set; }
        public Member Member { get; set; }
        public string Tag { get; set; } = Guid.NewGuid().ToString();

        public bool Approved { get; set; }
        public bool? HasReflected { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
