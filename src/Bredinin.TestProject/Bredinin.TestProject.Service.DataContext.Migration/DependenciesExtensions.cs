using Bredinin.TestProject.Service.DataContext.Migration.Metadata;
using Bredinin.TestProject.Service.DataContext.Migration.Migrations;
using FluentMigrator.Runner;
using FluentMigrator.Runner.VersionTableInfo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bredinin.TestProject.Service.DataContext.Migration
{
    public static class DependenciesExtensions
    {
        public static IServiceCollection AddMigration(this IServiceCollection services, IConfiguration configuration)
        {
            var connStr = configuration.GetConnectionString("DefaultConnection");

            services.AddFluentMigratorCore().ConfigureRunner(r => r.AddPostgres()
                    .WithGlobalConnectionString(connStr)
                    .WithRunnerConventions(new MigrationRunnerConventions())
                    .ScanIn(typeof(Migration_2024_10_14_0830).Assembly).For.Migrations())
                .AddLogging(l => l.AddFluentMigratorConsole());

            services.AddScoped(typeof(IVersionTableMetaData), typeof(VersionTableMetaData));

            return services;
        }
    }
}
