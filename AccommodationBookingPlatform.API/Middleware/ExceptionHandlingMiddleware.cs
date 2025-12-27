using AccommodationBookingPlatform.Application.Exceptions;
using System.Net;
using System.Text.Json;


namespace AccommodationBookingPlatform.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            object? errors = null;
            string title = "An error occurred";

            switch (exception)
            {
                case AccommodationBookingPlatform.Application.Exceptions.ValidationException ve:
                    statusCode = HttpStatusCode.BadRequest;
                    title = "Validation error";
                    errors = ve.Errors;
                    break;

                case FluentValidation.ValidationException fv:
                    statusCode = HttpStatusCode.BadRequest;
                    title = "Validation error";

                    errors = fv.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(e => e.ErrorMessage).ToArray()
                        );
                    break;

                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    title = "Resource not found";
                    break;

                case UnauthorizedException:
                    statusCode = HttpStatusCode.Unauthorized;
                    title = "Unauthorized";
                    break;

                case ForbiddenException:
                    statusCode = HttpStatusCode.Forbidden;
                    title = "Forbidden";
                    break;

                case ConflictException:
                    statusCode = HttpStatusCode.Conflict;
                    title = "Conflict";
                    break;

                default:
                    title = "Internal server error";
                    break;
            }



            var response = new
            {
                title,
                status = (int)statusCode,
                detail = exception.Message,
                errors
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}