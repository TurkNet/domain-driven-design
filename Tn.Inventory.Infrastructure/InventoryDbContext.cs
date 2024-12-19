using System.Collections.Immutable;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Tn.Inventory.Domain.Aggregates;
using Tn.Inventory.Domain.Entities;
using Tn.Inventory.Domain.Primitives;

namespace Tn.Inventory.Infrastructure;

public class InventoryDbContext : DbContext, IInventoryDbContext
{
    private readonly IPublisher _publisher;
    private List<IDomainEvent> _domainEvents;

    public InventoryDbContext(DbContextOptions<InventoryDbContext> options, IPublisher publisher)
        : base(options)
    {
        _publisher = publisher;
        
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DatabaseFacade Database => base.Database;

    public DbSet<Brand> Brands { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<ShipmentDetail> ShipmentDetails { get; set; }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        await PublishDomainEvents(cancellationToken);

        return result;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryDbContext).Assembly);
    }

    private async Task PublishDomainEvents(CancellationToken cancellationToken)
    {
        var domainEvents = new List<IDomainEvent>();

        ChangeTracker
            .Entries<Aggregate>()
            .Select(a => a.Entity)
            .SelectMany(a =>
            {
                domainEvents.AddRange(a.DomainEvents);

                a.ClearDomainEvents();

                return domainEvents;
            })
            .ToImmutableList();

        if (domainEvents.Any())
        {
            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }
        }
    }
}