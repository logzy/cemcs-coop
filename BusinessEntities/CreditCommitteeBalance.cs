using System.ComponentModel.DataAnnotations;

namespace COOP.Banking.BusinessEntities
{
    public class CreditCommitteeBalance
    {
        [Key]
        public string EMPNO { get; set; }
        public string EmpName { get; set; }
        public string Savgs { get; set; }
        public string SDEP { get; set; }
        public string STLoan { get; set; }
        public string LTLoan { get; set; }
        public string HAPL { get; set; }
        public string Vehicle { get; set; }
        public string TSL1 { get; set; }
        public string TSL2 { get; set; }
        public string TSL3 { get; set; }
        public string Executive { get; set; }
    }
}
