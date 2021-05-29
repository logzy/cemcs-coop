using COOP.Banking.BusinessEntities;
using System;

namespace COOP.Banking.Data.DTO
{
    [Serializable]
    public class LoanApplicationDTO
    {
        public int? Id { get; set; }
        public int MemberId { get; set; }
        public DateTime DateSubmitted { get; set; }
        public decimal LoanAmount { get; set; }
        public int RepaymentPeriod { get; set; }
        public decimal Principal { get; set; }
        public int Status { get; set; }
        public decimal Interest { get; set; }
        public Member member { get; set; }
    }
}
