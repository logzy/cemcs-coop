using System;

namespace COOP.Banking.BusinessEntities
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Sex { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Title { get; set; }
        public string WorkPhone { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string PersonalEmail { get; set; } //new
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int StateId { get; set; }
        public string Country { get; set; }
        public string MaritalStatus { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public State State { get; set; }

    }
}
