using COOP.Banking.BusinessEntities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COOP.Banking.Data.DTO
{
    public class EmployeeGetDTO
    {
        public int? Id { get; set; }
        public string UserId { get; set; }
        public int PersonId { get; set; }
        public string JobTitle { get; set; }
        public int DepartmentId { get; set; }
        public int StateOfOriginId { get; set; }
        public int EmployeeTypeId { get; set; }
        public DateTime DateOfHire { get; set; }
        public DateTime? ResignationDate { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(18,2)")]
        public decimal AnnualSalary { get; set; }

        [DataType(DataType.Currency), Column(TypeName = "decimal(18,2)")]
        public decimal BasicSalary { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public DateTime userLogin { get; set; }
        public DateTime DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public Person Person { get; set; }
        public Department Department { get; set; }
        public EmployeeType EmployeeType { get; set; }
    }
}