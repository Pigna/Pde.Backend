using Pde.Backend.Core.Extensions;

namespace Pde.Backend.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPdeBackendApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRazorPages();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddDependencies(configuration);
    }

    private static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPdeBackendCoreServices(configuration);
    }
}