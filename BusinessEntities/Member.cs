using COOP.Banking.Data;
using System;

namespace COOP.Banking.BusinessEntities
{
    public class Member
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string EmployeeNumber { get; set; }
        public Enums.MemberType MemberType { get; set; }
        public string UserId { get; set; }
        public string Location { get; set; }
        public DateTime EmploymentDate { get; set; } //new
        public DateTime RegistrationDate { get; set; }
        public int ApprovalCount { get; set; }
        public bool Approved { get; set; }
        public bool HasPaidFee { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }

        public Person Person { get; set; }

        public string Tag { get; set; }
        public DateTime RetirementDate { get; internal set; }
        //public MemberType MemberType { get; set; }
        // add user???
    }
}
