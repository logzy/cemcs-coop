using System;

namespace COOP.Banking.Data.DTO
{
    [Serializable]
    public class AccountEnquiryDTO
    {
        //public string account_bank { get; set; }
        //public string account_number { get; set; }
        public string destbankcode { get; set; }
        public string recipientaccount { get; set; }
        public string PBFPubKey { get; set; }
    }
}
