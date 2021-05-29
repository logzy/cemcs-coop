using System;

namespace COOP.Banking.Data.DTO
{
    [Serializable]
    public class AccountResponseDTO
    {
        public string status { get; set; }
        public AccountData data { get; set; }
    }
}
