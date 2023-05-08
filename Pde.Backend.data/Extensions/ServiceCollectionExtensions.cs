using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pde.Backend.Data.Database;
using Pde.Backend.Data.Database.Implementations;

namespace Pde.Backend.Data.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPdeBackendDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDbConnectionFactory, PostgresConnectionFactory>();
        services.AddTransient<IDbSchemaProvider, PostgresSchemaProvider>();
        services.AddDependencies(configuration);
    }

    private static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
    }
}