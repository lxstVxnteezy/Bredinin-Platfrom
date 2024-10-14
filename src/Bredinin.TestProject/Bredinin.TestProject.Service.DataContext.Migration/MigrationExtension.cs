using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Bredinin.TestProject.Service.DataContext.Migration
{
    public static class MigrationExtension
    {
        public static IMigrationRunner Migrate(this IMigrationRunner runner, IConfiguration configuration)
        {
            CreateDatabaseIfNotExist(configuration);

            if (runner.HasMigrationsToApplyUp())
            {
                runner.ListMigrations();
                runner.MigrateUp();
            }
            return runner;
        }

        private static void CreateDatabaseIfNotExist(IConfiguration configuration)
        {
            var defaultConnStr = configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
            var builder = new NpgsqlConnectionStringBuilder(defaultConnStr);
            var dbName = builder.Database;
            builder.Database = "postgres";
            var masterConnStr = builder.ToString();

            using NpgsqlConnection connection = new NpgsqlConnection(masterConnStr);
            connection.Open();

            using var checkIfExistCommand = new NpgsqlCommand(
                $"SELECT 1 FROM pg_catalog.pg_database WHERE datname = '{dbName}'",
                connection);

            var result = checkIfExistCommand.ExecuteScalar();

            if (result == null)
            {
                using var command = new NpgsqlCommand($"CREATE DATABASE \"{dbName}\"", connection);
                command.ExecuteNonQuery();
            }
        }
    }
}
