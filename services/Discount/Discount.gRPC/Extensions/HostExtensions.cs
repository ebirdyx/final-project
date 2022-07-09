using Npgsql;

namespace Discount.gRPC.Extensions;

public static class HostExtensions
{
    public static IHost MigrateDatabase<TContext>(this IHost host, int? retry = 0)
    {
        var retryForAvailability = retry.Value;

        using var scope = host.Services.CreateScope();

        var services = scope.ServiceProvider;
        var configuration = services.GetRequiredService<IConfiguration>();
        var logger = services.GetRequiredService<ILogger<TContext>>();

        try
        {
            logger.LogInformation("Migrating Postgres database"); 
            using var conn = new NpgsqlConnection(
                configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            conn.Open();
            
            using var command = new NpgsqlCommand { Connection = conn };

            command.CommandText = "DROP TABLE IF EXISTS Coupon";
            command.ExecuteNonQuery();

            command.CommandText = @"CREATE TABLE Coupon (
                ID SERIAL PRIMARY KEY NOT NULL,
                ProductName VARCHAR(100) NOT NULL,
                Description TEXT,
                Amount int
            );";
            command.ExecuteNonQuery();
            
            //TODO: Seed data
            
            logger.LogInformation("Migrated Postgres database");
        }
        catch (NpgsqlException e)
        {
            logger.LogError(e, "An error occured while migrating the postgresql database");
            
            if (retryForAvailability < 50)
            {
                retryForAvailability++;
                Thread.Sleep(2000);
                MigrateDatabase<TContext>(host, retryForAvailability);
            }
        }

        return host;
    }
}