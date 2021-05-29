using System;

namespace COOP.Banking.Data.DTO
{
    [Serializable]
    public class ModeOfPaymentDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
