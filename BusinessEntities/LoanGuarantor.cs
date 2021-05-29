using COOP.Banking.Data;
using System;

namespace COOP.Banking.BusinessEntities
{
    public class LoanGuarantor
    {
        public int Id { get; set; }

        public string EmployeeNumber { get; set; }
        public string GuarantorName { get; set; }
        public string GuarantorEmail { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public int LoanApplicationId { get; set; }
        public string Comments { get; set; }
        public Enums.ApprovalStatus ApprovalStatus { get; set; }
        public LoanApplication LoanApplication { get; set; }
    }
}
