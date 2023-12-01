using FM_MyStat.Core.DTOs.UsersDTO.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Services
{
    public class ServiceResponse<PayloadType, ErrorType>
    {
        public ServiceResponse(bool success = false, string message = "", PayloadType payload = default, IEnumerable<ErrorType> errors = default)
        {
            this.Success = success;
            this.Message = message;
            this.Payload = payload;
            this.Errors = errors ?? Enumerable.Empty<ErrorType>();
        }
        public bool Success { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public PayloadType Payload { get; set; } = default;
        public IEnumerable<ErrorType> Errors { get; set; } = Enumerable.Empty<ErrorType>();
    }
    public class ServiceResponse : ServiceResponse<object, object>
    {
        public ServiceResponse(bool success = false, string message = "", object payload = default, IEnumerable<object> errors = default)
            : base(success, message, payload, errors) { }
    }
}
