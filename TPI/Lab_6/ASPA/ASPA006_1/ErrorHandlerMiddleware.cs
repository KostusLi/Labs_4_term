using DAL_Celebrity_MSSQL;
using System.Net;
using System.Text.Json;

namespace ASPA006_1
{
    public class ErrorHandlerMiddleware: IMiddleware
    {

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex) {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                title = "Внутренняя ошибка сервера",
                detail = ex.Message,
                instance = context.Request.Path
            };

            var result = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(result);
        }
    }
}
