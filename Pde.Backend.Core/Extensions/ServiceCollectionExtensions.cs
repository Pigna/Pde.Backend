using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pde.Backend.Core.TableInfos.Services;
using Pde.Backend.Core.TableInfos.Services.Implementations;
using Pde.Backend.Data.Extensions;

namespace Pde.Backend.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPdeBackendCoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IExportService, ExportService>();
        services.AddTransient<ITableInfoService, TableInfoService>();
        services.AddTransient<IFakeDataService, FakeDataService>();
        services.AddDependencies(configuration);
    }

    private static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPdeBackendDataServices(configuration);
    }
}