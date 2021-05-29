using System.Collections.Generic;

namespace COOP.Banking.Data
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = null;
        public List<Error> Errors { get; set; }
    }
}