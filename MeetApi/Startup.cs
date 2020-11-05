using AutoMapper;
using MeetApi.Database;
using MeetApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Reflection;

namespace MeetApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(x =>
                x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            ConfigureContext(services);
            ConfigureLocalServices(services);
            ConfigureAuthentication(services);
            ConfigureAuthorization(services);
        }

        private static void ConfigureLocalServices(IServiceCollection services)
        {
            services.AddTransient<IDatabaseManager, LocalDbDatabaseManager>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IAuthorizer, JwtAuthorizer>();
            services.AddTransient<Methods>();
        }

        private void ConfigureAuthorization(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
        private void ConfigureAuthentication(IServiceCollection services)
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
        }

        private void ConfigureContext(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("DbConnection");
            services.AddDbContext<AppContext>(opt => opt.UseSqlServer(connectionString));
        }

    }
}