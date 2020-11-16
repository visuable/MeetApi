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
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.Swagger;
using System.IO;
using System.Reflection;
using MeetApi.MeetApi.Hubs;

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
            // services.AddSwaggerGen(c =>
            // {
            //     var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //     var xmlPath = Path.Combine(System.AppContext.BaseDirectory, xmlFile);
            //     //c.IncludeXmlComments(xmlPath, true);
            // });
            services.AddControllers().AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                x.SerializerSettings.TypeNameHandling = TypeNameHandling.All;
            });
            services.AddSignalR();
            var connectionString = "Data Source = (LocalDb)\\MSSQLLocalDB; Database = appEzDb;";
            services.AddDbContext<AppContext>(opt => opt.UseSqlServer(connectionString));
            ConfigureLocalServices(services);
            ConfigureAuthentication(services);
            ConfigureAuthorization(services);
        }

        private static void ConfigureLocalServices(IServiceCollection services)
        {
            services.AddTransient<IDatabaseManager, LocalDbDatabaseManager>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IAuthorizer, JwtAuthorizer>();
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
            // app.UseSwagger();
            // app.UseSwaggerUI(options =>
            // {
            //     options.SwaggerEndpoint("v1/swagger.json", "v1");
            // });
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MeetHub>("/meetings");
                endpoints.MapDefaultControllerRoute();
            });
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
    }
}