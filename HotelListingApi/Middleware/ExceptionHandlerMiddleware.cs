
using HotelListing.Domain.Exceptions;
using HotelListing.Domain.Models;
using Newtonsoft.Json;
using System.Net;

namespace HotelListing.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Something went wrong {context.Request.Path}");
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

            var errorDetails = new ErrorDetails
            {
                ErrorType = "Failure",
                ErrorMessage = exception.Message,

            };

            switch (exception)
            {
                case NotFoundCustomException notFoundExceptionCustom:
                    statusCode = HttpStatusCode.NotFound;
                    errorDetails.ErrorType = "Not Found";
                    break;
                case ConflictCustomException conflictCustomException:
                    statusCode = HttpStatusCode.Conflict;
                    errorDetails.ErrorType = "Conflict";
                    break;
                default:
                    break;
            }

            string response = JsonConvert.SerializeObject(errorDetails);
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(response);

        }
    }
}
