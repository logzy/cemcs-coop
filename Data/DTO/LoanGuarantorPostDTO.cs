using System;

namespace COOP.Banking.Data.DTO
{
    [Serializable]
    public class LoanGuarantorPostDTO
    {
        public string EmployeeNumber { get; set; }
        public string GuarantorName { get; set; }
        public string GuarantorEmail { get; set; }
    }
}
