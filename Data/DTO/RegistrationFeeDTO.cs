using System;

namespace COOP.Banking.Data.DTO
{
    [Serializable]
    public class RegistrationFeeDTO
    {
        public int? Id { get; set; }
        public int MemberTypeId { get; set; }
        public decimal Amount { get; set; }
    }
}
