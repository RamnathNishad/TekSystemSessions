
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EmployeeAPI
{
    public class GlobalExceptionHandler : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                ProblemDetails details = new ProblemDetails
                {
                  Status=(int)System.Net.HttpStatusCode.InternalServerError,
                  Detail="Some error occurred:" + ex.Message,
                  Type="Server error",
                  Title="Internal server error"
                };
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(details));
            }
        }
    }
}
