using System;

namespace COOP.Banking.Data.DTO
{
    [Serializable]
    public class BankDTO
    {
        public int? Id { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
    }
}
