using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace MeetApi.MeetApi.Services
{
    public static class Extensions
    {
        public static IServiceCollection ConfigureLocalServices(this IServiceCollection services)
        {
            services.AddTransient<IDatabaseManager, LocalDbDatabaseManager>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IAuthorizer, JwtAuthorizer>();
            return services;
        }

        public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });
            return services;
        }

        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateActor = true,
                    ValidateIssuerSigningKey = true,
                    ValidateTokenReplay = true,
                    ValidIssuer = TokenSettings.Issuer,
                    ValidAudience = TokenSettings.Audience,
                    IssuerSigningKey = TokenSettings.GetSymmetricKey()
                };
            });
            return services;
        }
    }
}