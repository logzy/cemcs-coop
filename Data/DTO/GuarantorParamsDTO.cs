using System;

namespace COOP.Banking.Data.DTO
{
    [Serializable]
    public class GuarantorParamsDTO
    {
        public int MemberId { get; set; }
        public int LoanId { get; set; }

        public decimal LoanAmount { get; set; }
    }
}
