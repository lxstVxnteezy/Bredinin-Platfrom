using Bredinin.TestProject.DataContext.DataAccess;
using Bredinin.TestProject.Service.Api.Middlewares.Http;
using Bredinin.TestProject.Service.Core.Swagger;
using Bredinin.TestProject.Service.DataContext.Migration;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Bredinin.TestProject.Service.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        { 
            services.AddControllers();
            services.AddHealthChecks();
            services.AddDataAccess(_configuration);
            services.AddMigration(_configuration);
            services.AddSwagger();
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
            app.UseAuthorization();
            app.Migrate(_configuration);

        }


    }
}
