using COOP.Banking.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COOP.Banking.BusinessEntities
{
    public class LoanApplication
    {
        public LoanApplication()
        {
            LoanGuarantors = new HashSet<LoanGuarantor>();
        }
        public int Id { get; set; }
        public int LoanId { get; set; }
        public int MemberId { get; set; }
        public DateTime DateSubmitted { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(10,2)")]
        public decimal LoanAmount { get; set; }
        public int RepaymentPeriod { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(10,2)")]
        public decimal Principal { get; set; }
        public int Status { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(10,2)")]
        public decimal Interest { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public int MethodOfCollectionId { get; set; }
        public int ApprovalCount { get; set; }
        public int GuarantorApprovalCount { get; set; }
        public int RequiredGuarantorsCount { get; set; }
        public string BankName { get; set; }
        public string BankCode { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public Enums.LoanConditionStatus LoanCondition { get; set; }
        public bool IsPaid { get; set; }
        public string Tag { get; set; }
        public bool Approved { get; set; }
        public Loan Loan { get; set; }
        public Member Member { get; set; }

        public string filePath { get; set; }

        public ICollection<LoanGuarantor> LoanGuarantors { get; set; }
    }
}
