using MediatR;
using Microsoft.EntityFrameworkCore;
using Tn.Inventory.Domain.Aggregates;
using Tn.Inventory.Domain.Events;
using Tn.Inventory.Infrastructure;

namespace Tn.Inventory.Application.Purchases.Events;

public class PurchaseCompletedEventHandler : INotificationHandler<PurchaseCompletedEvent>
{
    private readonly IInventoryDbContext _context;

    public PurchaseCompletedEventHandler(IInventoryDbContext context)
    {
        _context = context;
    }

    public async Task Handle(PurchaseCompletedEvent notification, CancellationToken cancellationToken)
    {
        var purchase = await _context.Purchases
            .Where(a => a.Id == notification.Id)
            .Include(a => a.PurchaseDetails)
            .FirstOrDefaultAsync(cancellationToken);

        if (purchase is null)
            return;

        var stocks = purchase.PurchaseDetails
            .Select(a => new Stock(a.Purchase.TargetWarehouseId!.Value, a.DeviceId, a.Quantity, purchase.CreatedBy!))
            .ToList();

        await _context.Stocks.AddRangeAsync(stocks, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}