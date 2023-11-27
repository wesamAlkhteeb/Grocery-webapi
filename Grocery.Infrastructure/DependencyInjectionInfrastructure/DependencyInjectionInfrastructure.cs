using Grocery.Application.Repository;
using Grocery.Domain.Helper;
using Grocery.Infrastructure.DatabaseContext;
using Grocery.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grocery.Infrastructure.DependencyInjectionInfrastructure
{
    public static class DependencyInjectionInfrastructure
    {
        public static IServiceCollection DIInfrastructure(IServiceCollection service)
        {
            service.AddScoped<IAccountRepository, AccountRepository>();
            return service;
        }

        public static IServiceCollection DIMongoDb(IServiceCollection service , string connectionString , string dbName)
        {
            IMongoClient client = new MongoClient("mongodb://localhost:6000");
            IMongoDatabase db = client.GetDatabase("Grocery"); 
            service.AddSingleton<DbContext>(new DbContext(db));
            return service;
        }
        public static IServiceCollection DISecurity(this IServiceCollection services, IConfiguration configuration)
        {
            var keyJwt = configuration["JWT_Key"]!;
            var issuerJwt = configuration["JWT_Issuer"]!;
            var audienceJwt = configuration["JWT_Audience"]!;
            var durationExpiredInDay_JWT = configuration["JWT_DurationExpiredInDay"]!;

            services.Configure<JwtHelper>(jwt => {
                jwt.Key = keyJwt;
                jwt.Audience = audienceJwt;
                jwt.Issuer = issuerJwt;
                jwt.DurationExpiredInDay = int.Parse(durationExpiredInDay_JWT);
            });
            services.AddAuthentication(option => {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(
                op =>
                {
                    op.RequireHttpsMetadata = false;
                    op.SaveToken = false;
                    op.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = issuerJwt,
                        ValidAudience = audienceJwt,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyJwt)),
                        ClockSkew = TimeSpan.Zero
                    };
                });
            return services;
        }

    }
}
