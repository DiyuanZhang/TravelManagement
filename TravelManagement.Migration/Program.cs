using System;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace TravelManagement.Migration
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = CreateServices(args[0]);

            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        private static IServiceProvider CreateServices(string DBConnectionString)
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer2012()
                    .WithGlobalConnectionString(
                        DBConnectionString)
                    .ScanIn(typeof(CreateOrdersTable).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}
