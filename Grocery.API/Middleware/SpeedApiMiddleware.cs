using System.Diagnostics;

namespace Grocery.API.Middleware
{
    public class SpeedApiMiddleware
    {
        private RequestDelegate _next;
        private ILogger _log;
        public SpeedApiMiddleware(RequestDelegate request , ILogger<SpeedApiMiddleware> logger) { 
            this._next = request;
            this._log = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            await _next(httpContext);
            stopwatch.Stop();
            _log.LogInformation($"The time for this request is {stopwatch.ElapsedMilliseconds}ms");
        }
    }   
}
