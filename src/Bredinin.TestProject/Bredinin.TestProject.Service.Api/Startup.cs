using Bredinin.TestProject.DataContext.DataAccess;
using Bredinin.TestProject.Service.DataContext.Migration;

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
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


        }


    }
}
