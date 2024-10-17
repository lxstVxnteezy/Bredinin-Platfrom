using Bredinin.TestProject.DataContext.DataAccess.Repositories;
using Bredinin.TestProject.Service.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bredinin.TestProject.DataContext.DataAccess
{
    public static class DependenciesExtensions
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            var connStr = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ServiceContext>(options => options.UseNpgsql(connStr!));

            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            return services;
        }

    }
}
