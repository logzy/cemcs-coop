using COOP.Banking.BusinessEntities;
using System;

namespace COOP.Banking.Data.DTO
{
    [Serializable]
    public class GetMemEmpNumberDTO
    {
        public string EmployeeNumber { get; set; }
        public Person person { get; set; }
    }
}
