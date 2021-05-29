using System;

namespace COOP.Banking.Data.DTO
{
    public class TransferSaveDTO
    {
        public int MemberId { get; set; }

        public Enums.SavingsType SourceSavingsType { get; set; }
        public Enums.SavingsType DestinationSavingsType { get; set; }


        public DateTime EffectiveDate { get; set; }
        public decimal Amount { get; set; }

        public string CreatedBy { get; set; }

    }
}
