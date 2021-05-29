using System;

namespace COOP.Banking.Data.DTO
{
    [Serializable]
    public class GuarantorApproverDTO
    {
        public int LoanApplicationId { get; set; }
        public string EmployeeNumber { get; set; }
        public string Comments { get; set; }
        public int Status { get; set; }
    }
}
