using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Tn.Inventory.Domain.Aggregates;
using Tn.Inventory.Domain.Entities;

namespace Tn.Inventory.Infrastructure;

public interface IInventoryDbContext
{
    DatabaseFacade Database { get; }

    DbSet<Brand> Brands { get; set; }
    DbSet<Device> Devices { get; set; }
    DbSet<Supplier> Suppliers { get; set; }
    DbSet<Warehouse> Warehouses { get; set; }
    DbSet<Stock> Stocks { get; set; }
    DbSet<Purchase> Purchases { get; set; }
    DbSet<PurchaseDetail> PurchaseDetails { get; set; }
    DbSet<Shipment> Shipments { get; set; }
    DbSet<ShipmentDetail> ShipmentDetails { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}