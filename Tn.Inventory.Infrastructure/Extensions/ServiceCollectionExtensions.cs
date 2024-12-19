using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Tn.Inventory.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInventoryDbContext(this IServiceCollection services, string connectionString, ServiceLifetime lifetime)
    {
        ArgumentNullException.ThrowIfNull(nameof(services));

        if (string.IsNullOrEmpty(connectionString))
            throw new ArgumentNullException(nameof(connectionString));

        services.AddDbContext<InventoryDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
            options.EnableSensitiveDataLogging();
        }, lifetime);

        services.Add(new ServiceDescriptor(typeof(IInventoryDbContext), typeof(InventoryDbContext), lifetime));
    }
}