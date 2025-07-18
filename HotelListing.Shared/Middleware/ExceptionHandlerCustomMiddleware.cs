using HotelListing.Application.Models;
using HotelListing.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace HotelListing.Shared.Middleware
{
    public class ExceptionHandlerCustomMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerCustomMiddleware> _logger;

        public ExceptionHandlerCustomMiddleware(RequestDelegate next, ILogger<ExceptionHandlerCustomMiddleware> logger)
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
                case UnauthorizedAccessCustomException unauthorizedAccessCustomException:
                    statusCode = HttpStatusCode.Unauthorized;
                    errorDetails.ErrorType = "Unauthorized";
                    break;
                case ApplicationCustomException applicationCustomException:
                    statusCode = HttpStatusCode.BadRequest;
                    errorDetails.ErrorType = "Bad Request";
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
