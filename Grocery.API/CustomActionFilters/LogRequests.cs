using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Text.Json;

//Action  Filter attribute
namespace Grocery.API.CustomActionFilters
{
    public class LogRequests : IAsyncActionFilter
    {
        private readonly ILogger<LogRequests> logger;

        //public LogRequests(ILogger<LogRequests> logger)
        //{
        //    this.logger = logger;
        //}

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //before request
            //context.Result = new NotFoundResult(); //500  // set result before excute api mean not allow to run api where there are result.
            //logger.LogInformation(JsonSerializer.Serialize(context.ActionArguments)); all data pass with path
            //Console.WriteLine(context.Controller); name controller
            //context.ActionDescriptor //name  using function ( get - post - delete - put )
            await next();
            //after request
        }
    }
}


/*
 action filter
ways to registeration

    builder.Services.AddControllers(options =>
    {
        options.Filters.Add<LogRequests>();
    });
 
 */