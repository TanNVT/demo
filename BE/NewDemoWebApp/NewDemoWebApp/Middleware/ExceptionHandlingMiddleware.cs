using NewDemoWebApp.Common;
using Newtonsoft.Json;
using System.Net;

namespace NewDemoWebApp.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Call the next middleware in the pipeline
                await _next(context);
            }
            catch (ApiException e)
            {
                // Set the status code and content type
                context.Response.StatusCode = e.StatusCode;
                context.Response.ContentType = "application/json";

                // Serialize the value to JSON
                var result = JsonConvert.SerializeObject(new { e.ErrorCode, e.MessageContent, e.Content });

                // Write the JSON response to the body
                await context.Response.WriteAsync(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");

                // Set the response status code
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                // Set the response content type to JSON
                context.Response.ContentType = "application/json";

                // Create an API error response
                var apiResponse = new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = ex?.Message ?? "An error occurred. Please try again later."
                };

                // Serialize the response to JSON
                var jsonResponse = JsonConvert.SerializeObject(apiResponse);

                // Write the JSON response to the response body
                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
