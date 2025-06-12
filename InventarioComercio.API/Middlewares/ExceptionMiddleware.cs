using InventarioComercio.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace InventarioComercio.API.Middlewares
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // sigue con la siguiente etapa del pipeline
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error capturado por middleware: {ex.Message}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            object response;

            switch (exception)
            {
                case NotFoundException:
                    status = HttpStatusCode.NotFound;
                    response = new
                    {
                        error = exception.Message,
                        statusCode = (int)HttpStatusCode.NotFound
                    };
                    break;

                case BadRequestException:
                    status = HttpStatusCode.BadRequest;
                    response = new
                    {
                        error = exception.Message,
                        statusCode = (int)HttpStatusCode.BadRequest
                    };
                    break;

                case ValidationException validationEx:
                    status = HttpStatusCode.BadRequest;
                    response = new
                    {
                        error = validationEx.Message,
                        errors = validationEx.Errors,
                        statusCode = (int)HttpStatusCode.BadRequest
                    };
                    break;

                default:
                    status = HttpStatusCode.InternalServerError;
                    response = new
                    {
                        error = "Se produjo un error en el servidor.",
                        statusCode = (int)HttpStatusCode.InternalServerError
                    };
                    break;
            }
           

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            var json = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(json);
        }
    }
}
