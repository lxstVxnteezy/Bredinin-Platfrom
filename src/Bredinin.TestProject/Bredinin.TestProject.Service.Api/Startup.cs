using Bredinin.TestProject.DataContext.DataAccess;
using Bredinin.TestProject.Service.Api.Middlewares.Http;
using Bredinin.TestProject.Service.Core.Swagger;
using Bredinin.TestProject.Service.Core.Validation;
using Bredinin.TestProject.Service.DataContext.Migration;
using Bredinin.TestProject.Service.Http.Handlers;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Bredinin.TestProject.Service.Api
{
    public class Startup
    {
        public IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        { 
            services.AddControllers();
            services.AddHealthChecks();
            services.AddDataAccess(Configuration);
            services.AddMigration(Configuration);
            services.AddSwagger();
            services.AddHttpHandlers();
            services.AddValidation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
                app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });

            app.UseHttpsRedirection();
            app.UseSwaggerCustom();
            app.Migrate(Configuration);    

        }


    }
}
