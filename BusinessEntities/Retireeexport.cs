using System.ComponentModel.DataAnnotations;

namespace COOP.Banking.BusinessEntities
{
    public class Retireeexport
    {
        [Key]
        public string EMPNO { get; set; }
        public string PINNO { get; set; }
        public string EMPLOYEENO { get; set; }
        public string JOBTITLE { get; set; }
        public string DIVISIONNAME { get; set; }
        public string SURNAME { get; set; }
        public string FIRSTNAME { get; set; }
        public string MIDDLENAME { get; set; }
        public string EMAILADDRESS { get; set; }
        public string ALTEMAILADDRESS { get; set; }
        public string DIRECTPHONE { get; set; }
        public string ALTPHONE { get; set; }
        public string LOCATIONNAME { get; set; }
        public string RETIREMENTDATE { get; set; }
        public string EMPLOYMENTDATE { get; set; }
        public string NATIONALITY { get; set; }
    }
}
