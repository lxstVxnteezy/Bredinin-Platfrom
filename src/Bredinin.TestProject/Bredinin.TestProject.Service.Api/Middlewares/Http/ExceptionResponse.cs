using System.Net;

namespace Bredinin.TestProject.Service.Api.Middlewares.Http
{
    public record ExceptionResponse(
        HttpStatusCode StatusCode,
        int Code,
        string UserMessage,
        string? InnerMessage = null);
}
