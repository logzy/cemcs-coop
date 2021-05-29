using COOP.Banking.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COOP.Banking.BusinessEntities
{
    public class TransferApplication
    {
        public int Id { get; set; }
        public int MemberId { get; set; }

        public Enums.SavingsType SourceSavingsType { get; set; }
        public Enums.SavingsType DestinationSavingsType { get; set; }

        public DateTime TransactionDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        [DataType(DataType.Currency), Column(TypeName = "decimal(10,2)")]
        public decimal Amount { get; set; }



        public int ApprovalCount { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public DateTime? LastApprovalDate { get; set; }
        public string CreatedBy { get; set; }

        public Member Member { get; set; }
    }
}
