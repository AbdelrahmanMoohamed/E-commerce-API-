using Microsoft.OpenApi.Exceptions;

namespace Talabat.APIs.Errors
{
    public class ApiExceptionMiddleWare:ApiResponse
    {
        public string? Details { get; set; }

        public ApiExceptionMiddleWare(int statuesCode, string? ErrorMessage = null, string? details = null) : base(statuesCode, ErrorMessage)
        {
            Details = details;
        }
    }
}
