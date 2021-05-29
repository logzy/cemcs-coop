using COOP.Banking.BusinessEntities;
using System;

namespace COOP.Banking.Data.DTO
{
    [Serializable]
    public class MemberDTO
    {
        public int? Id { get; set; }
        // public int PersonId { get; set; }
        public string EmployeeNumber { get; set; }
        public Enums.MemberType MemberType { get; set; }
        public string UserId { get; set; }
        public DateTime userLogin { get; set; }
        public string Location { get; set; }
        public DateTime EmploymentDate { get; set; } //new
        public int ApprovalCount { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
        public bool Approved { get; set; }
        public bool HasPaidFee { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public Person Person { get; set; }

        //Approval
        public bool IsCreditCommittee { get; set; }

        public decimal SavingsAmount { get; set; }
    }
}
