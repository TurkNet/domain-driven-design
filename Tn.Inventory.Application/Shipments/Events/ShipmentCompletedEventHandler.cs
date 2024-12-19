using MediatR;
using Microsoft.EntityFrameworkCore;
using Tn.Inventory.Domain.Aggregates;
using Tn.Inventory.Domain.Events;
using Tn.Inventory.Infrastructure;

namespace Tn.Inventory.Application.Shipments.Events;

public class ShipmentCompletedEventHandler : INotificationHandler<ShipmentCompletedEvent>
{
    private readonly IInventoryDbContext _context;

    public ShipmentCompletedEventHandler(IInventoryDbContext context)
    {
        _context = context;
    }

    public async Task Handle(ShipmentCompletedEvent notification, CancellationToken cancellationToken)
    {
        var shipment = await _context.Shipments
            .Where(a => a.Id == notification.Id)
            .Include(a => a.ShipmentDetails)
            .FirstOrDefaultAsync(cancellationToken);

        if (shipment is null)
            return;

        var deviceIds = shipment.ShipmentDetails.Select(a => a.DeviceId).ToList();
        var stocksInSourceWarehouse = await _context.Stocks
            .Where(a => a.WarehouseId == shipment.SourceWarehouseId && deviceIds.Contains(a.DeviceId))
            .ToListAsync(cancellationToken);

        if (!stocksInSourceWarehouse.Any())
            return;

        var targetWarehouse = await _context.Warehouses.FirstOrDefaultAsync(a => a.Id == shipment.TargetWarehouseId, cancellationToken);
        if (targetWarehouse is null)
            return;

        var stocksInTargetWarehouse = await _context.Stocks
            .Where(a => a.WarehouseId == shipment.TargetWarehouseId && deviceIds.Contains(a.DeviceId))
            .ToListAsync(cancellationToken);

        foreach (var demand in shipment.ShipmentDetails)
        {
            var stockInSourceWarehouse = stocksInSourceWarehouse.FirstOrDefault(a => a.DeviceId == demand.DeviceId);
            if (stockInSourceWarehouse is not null && stockInSourceWarehouse.Quantity >= demand.Quantity)
            {
                stockInSourceWarehouse.ReduceStock(demand.Quantity, shipment.CreatedBy!);
                _context.Stocks.Update(stockInSourceWarehouse);
            }

            var stockInTargetWarehouse = stocksInTargetWarehouse.FirstOrDefault(a => a.DeviceId == demand.DeviceId);
            if (stockInTargetWarehouse is null)
            {
                var newStock = new Stock(demand.Shipment.TargetWarehouseId, demand.DeviceId, demand.Quantity, shipment.CreatedBy!);
                _context.Stocks.Add(newStock);
            }
            else
            {
                stockInTargetWarehouse.IncreaseStock(demand.Quantity, shipment.CreatedBy!);
                _context.Stocks.Update(stockInTargetWarehouse);
            }
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}