using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace LoggingInKibana.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Skip irrelevant paths
            var path = context.Request.Path.ToString();
            if (path.StartsWith("/swagger") || path == "/favicon.ico")
            {
                await _next(context);
                return;
            }

            var stopwatch = Stopwatch.StartNew();

            // --- Transaction ID ---
            var transactionId = context.Request.Headers.ContainsKey("TransactionId")
                ? context.Request.Headers["TransactionId"].ToString()
                : Guid.NewGuid().ToString();

            // --- Read request body ---
            string requestBody = "(empty)";
            if (context.Request.ContentLength > 0 &&
                (context.Request.Method == "POST" || 
                 context.Request.Method == "PUT" || 
                 context.Request.Method == "PATCH"))
            {
                context.Request.EnableBuffering();
                using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
                requestBody = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0; // reset for controller
            }

            // --- Capture response ---
            var originalBodyStream = context.Response.Body;
            await using var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            await _next(context); // call controller

            stopwatch.Stop();

            // --- Read response body ---
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            string responseBody = "(empty)";
            using (var reader = new StreamReader(context.Response.Body, Encoding.UTF8, leaveOpen: true))
            {
                responseBody = await reader.ReadToEndAsync();
            }

            // Reset position to allow response to be sent to client
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            await responseBodyStream.CopyToAsync(originalBodyStream);

            // --- Log structured information ---
            Log.ForContext("TransactionId", transactionId)
               .ForContext("Method", context.Request.Method)
               .ForContext("Path", path)
               .ForContext("QueryString", context.Request.QueryString.ToString())
               .ForContext("RequestBody", string.IsNullOrWhiteSpace(requestBody) ? "(empty)" : requestBody)
               .ForContext("ResponseBody", string.IsNullOrWhiteSpace(responseBody) ? "(empty)" : responseBody)
               .ForContext("StatusCode", context.Response.StatusCode)
               .ForContext("DurationMs", stopwatch.ElapsedMilliseconds)
               .Information("HTTP Request/Response logged");
        }
    }
}
