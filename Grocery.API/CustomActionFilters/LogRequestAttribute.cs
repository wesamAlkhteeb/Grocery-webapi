using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;


//Action  Filter attribute
namespace Grocery.API.CustomActionFilters
{
    public class LogRequestAttribute:ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();
            Debug.WriteLine("Sesitive data !!!!!");
        }
    }
}


/*
 1- it's attribute as HttpGet define before method or classController
 2- builder.Services.AddControllers(options =>
    {
        options.Filters.Add<LogRequests>();
    });
 */