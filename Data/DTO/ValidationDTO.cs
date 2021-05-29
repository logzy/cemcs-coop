using System;
using System.Collections.Generic;

namespace COOP.Banking.Data.DTO
{
    [Serializable]
    public class ValidationDTO
    {
        public bool Status { get; set; }
        public List<Error> Errors { get; set; }
    }
}
