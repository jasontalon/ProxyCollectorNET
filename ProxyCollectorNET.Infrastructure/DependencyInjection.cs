using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ProxyCollectorNET.Infrastructure.Database;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddDbContext<AppDbContext>(builder =>
            builder.UseInMemoryDatabase(nameof(AppDbContext)));
        serviceCollection.AddHttpClient();

        return serviceCollection;
    }
}