using System;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using advicetest.Contracts;

namespace advicetest.Infrastructure.Middleware
{
	public class ErrorHandlingMiddleware
	{
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Вызывается при переходе обработки запроса данному звену
        /// </summary>
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                httpContext.Response.ContentType = MediaTypeNames.Application.Json;
                var error = new ErrorContract()
                {
                    Error = !String.IsNullOrEmpty(exception.Message)
                        ? exception.Message
                        : exception.ToString()
                };
                await httpContext.Response.WriteAsync(JsonSerializer.Serialize(error));
            }
        }
    }
}
