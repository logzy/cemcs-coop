using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COOP.Banking.Data.DTO
{
    [Serializable]
    public class PaymentDTO
    {
        public int? Id { get; set; }
        public string ReferenceCode { get; set; }
        public string EmployeeNumber { get; set; }
        public decimal Amount { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string Bank { get; set; }
        public Enums.PaymentStatus Status { get; set; }
        public string PayerEmployeeNumber { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
