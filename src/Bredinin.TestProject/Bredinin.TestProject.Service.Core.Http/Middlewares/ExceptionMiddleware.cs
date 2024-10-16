using System.Net;
using Microsoft.AspNetCore.Http;

namespace Bredinin.TestProject.Service.Core.Http.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            ExceptionResponse response = exception switch
            {
                OwnException ownException => new ExceptionResponse(
                    HttpStatusCode.UnprocessableEntity,
                    ownException.Code,
                    ownException.UserMessage,
                    ownException.InnerMessage),

                ApplicationException _ => new ExceptionResponse(
                    HttpStatusCode.BadRequest,
                    ReservedCodeError.UnexpectedError,
                    "Application exception occurred."),

                UnauthorizedAccessException _ => new ExceptionResponse(
                    HttpStatusCode.Unauthorized,
                    ReservedCodeError.UnexpectedError,
                    "Unauthorized"),

                _ => new ExceptionResponse(
                    HttpStatusCode.InternalServerError,
                    ReservedCodeError.UnexpectedError,
                    "Internal server error. Please retry later.")
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)response.StatusCode;

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
