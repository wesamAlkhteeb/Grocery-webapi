using Grocery.Domain.Models.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;

namespace Grocery.API.Middleware
{
    public class HandleErrorMiddleware : Controller
    {
        private readonly RequestDelegate next;

        public HandleErrorMiddleware(RequestDelegate _next)
        {
            next = _next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (BadHttpRequestException exception)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(new ResponseExceptionModel
                {
                    Message = exception.Message.ToString(),
                }));
            }catch(Exception exception)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(new ResponseExceptionModel
                {
                    Message = exception.Message.ToString(),
                }));
            }
        }
    }
}
