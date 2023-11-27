using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Grocery.API.Middleware
{
    public class AuthenticationImageMiddleware
    {
        private readonly RequestDelegate next;

        public AuthenticationImageMiddleware(RequestDelegate next)
        {
            this.next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value!=null && context.Request.Path.Value.ToLower().StartsWith("/images"))
            {
                if (context.User.Identity == null || !context.User.Identity.IsAuthenticated)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsJsonAsync<Dictionary<string,object>>(new Dictionary<string, object>
                    {
                        {"message","You must to login." }
                    });

                }
            }
            await next(context);
        }
    }
}
