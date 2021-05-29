using System.ComponentModel.DataAnnotations;

namespace COOP.Banking.BusinessEntities
{
    public class RetireeBalance
    {
        [Key]
        public string EMPNO { get; set; }
        public string NAME { get; set; }
        public string SAVINGS { get; set; }
        public string SPECDEP { get; set; }
        public string SHORTTERM { get; set; }
        public string LONGTERM { get; set; }
    }
}
