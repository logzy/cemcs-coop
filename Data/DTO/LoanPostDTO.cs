using System;
using System.Collections.Generic;

namespace COOP.Banking.Data.DTO
{
    [Serializable]
    public class LoanPostDTO
    {

        public int MemberId { get; set; }
        public float InterestRate { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal ExpectedAmount { get; set; }
        public int EffectiveMonth { get; set; }
        public int EffectiveYear { get; set; }
        public int RepaymntPeriod { get; set; }
        public int BankId { get; set; }
        public string BankCode { get; set; }
        public int MethodOfCollection { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public int Action { get; set; }
        public Enums.FormAction FormAction { get; set; }
        public int LoanId { get; set; }

        public List<LoanGuarantorPostDTO> LoanGuarantors { get; set; }
    }
}
