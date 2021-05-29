using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COOP.Banking.BusinessEntities
{
    public class SavingDepositLedger
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int SavingsType { get; set; }

        public DateTime TransactionDate { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(10,2)")]
        public decimal PreviousBalance { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(10,2)")]
        public decimal DepositAmount { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(10,2)")]
        public decimal CurrentBalance { get; set; }
        public int Status { get; set; }
        public int TransactionTypeId { get; set; }

        public TransactionType TransactionType { get; set; }
        public Member Member { get; set; }
    }
}
