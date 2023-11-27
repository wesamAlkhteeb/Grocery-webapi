using Grocery.API.CustomActionFilters;
using Grocery.API.HealthCheck;
using Grocery.API.Middleware;
using Grocery.Application.DependencyInjection;
using Grocery.Infrastructure.DependencyInjectionInfrastructure;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(
    options =>
    {
        options.Filters.Add<ValidationActionFilter>();
    }    
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


DependencyInjectionApplication.DIApplication(builder.Services);
DependencyInjectionInfrastructure.DIInfrastructure(builder.Services);
DependencyInjectionInfrastructure.DISecurity(builder.Services,builder.Configuration);
DependencyInjectionInfrastructure.DIMongoDb(builder.Services, builder.Configuration["connectionString"]!, builder.Configuration["dbName"]!);

//manage versions
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = false;
    options.ApiVersionReader = new QueryStringApiVersionReader("api-version");
    options.ReportApiVersions = true;
    
});
//health check
builder.Services.AddHealthChecks().AddCheck<MongodbHealthCheck>("mongodb");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<SpeedApiMiddleware>();
app.UseHttpsRedirection();
app.UseMiddleware<AuthenticationImageMiddleware>();
app.UseStaticFiles();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.MapHealthChecks("health",new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});;
app.UseMiddleware<HandleErrorMiddleware>();
app.Run();
