using System;

namespace COOP.Banking.BusinessEntities
{
    public class VoucherTransaction
    {
        public int Id { get; set; }
        public string VoucherNumber { get; set; }
        public DateTime VoucherDate { get; set; }
        public int Type { get; set; }
        public DateTime TransactionDate { get; set; }
        public int TransactionTypeId { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }

        public TransactionType TransactionType { get; set; }
    }
}
