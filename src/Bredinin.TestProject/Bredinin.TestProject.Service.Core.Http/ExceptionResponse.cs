using System.Net;

namespace Bredinin.TestProject.Service.Core.Http
{
    public record ExceptionResponse(
        HttpStatusCode StatusCode,
        int Code,
        string UserMessage,
        string? InnerMessage = null);
}
