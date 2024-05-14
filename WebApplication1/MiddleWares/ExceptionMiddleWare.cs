using System.Net;
using System.Text.Json;
using Talabat.APIs.Errors;

namespace Talabat.APIs.MiddleWares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleWare(RequestDelegate Next, ILogger<ExceptionMiddleWare> logger, IHostEnvironment env)
        {
            _next = Next;
            _logger = logger;
            _env = env;
        }

        //Invoke

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex , ex.Message);

                //Production => Log Exception in DB
                context.Response.ContentType = "application/json";
                // context.Response.StatusCode = 500;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //if (_env.IsDevelopment())
                //{
                //    var Response = new ApiExceptionMiddleWare(500, ex.Message, ex.StackTrace.ToString());
                //}
                //else
                //{
                //    var Response = new ApiExceptionMiddleWare((int)HttpStatusCode.InternalServerError);
                //}

                var Response = _env.IsDevelopment() ? new ApiExceptionMiddleWare(500, ex.Message, ex.StackTrace.ToString()) : new ApiExceptionMiddleWare((int)HttpStatusCode.InternalServerError);
                var Option = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var JsonResponse = JsonSerializer.Serialize(Response);
                context.Response.WriteAsync(JsonResponse);
            }
        }
    }

}
