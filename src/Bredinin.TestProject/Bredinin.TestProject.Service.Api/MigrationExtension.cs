using Bredinin.TestProject.Service.DataContext.Migration;
using FluentMigrator.Runner;

namespace Bredinin.TestProject.Service.Api
{
    public static class MigrationExtension
    {
        public static IApplicationBuilder Migrate(this IApplicationBuilder app, IConfiguration configuration)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var runner = scope.ServiceProvider.GetService<IMigrationRunner>();

            runner!.Migrate(configuration);

            runner!.ListMigrations();
            runner.MigrateUp();
            return app;
        }
    }
}
