using MeetApi.MeetApi.Database;
using MeetApi.MeetApi.Hubs;
using MeetApi.MeetApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace MeetApi.MeetApi
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
            var connectionString = "Data Source = (LocalDb)\\MSSQLLocalDB; Database = appEzDb;";
            services.AddDbContext<AppContext>(opt => opt.UseSqlServer(connectionString));
            services.AddControllers().AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                x.SerializerSettings.TypeNameHandling = TypeNameHandling.All;
            });
            services.AddSignalR();
            services.ConfigureLocalServices().ConfigureAuthentication().ConfigureAuthorization();
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
    }
}