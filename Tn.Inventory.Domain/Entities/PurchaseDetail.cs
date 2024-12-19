using Tn.Inventory.Domain.Aggregates;
using Tn.Inventory.Domain.Primitives;

namespace Tn.Inventory.Domain.Entities;

public sealed class PurchaseDetail : Entity
{
    public PurchaseDetail(int lineNumber, int purchaseId, int deviceId, int quantity, string createdBy)
    {
        LineNumber = lineNumber;
        PurchaseId = purchaseId;
        DeviceId = deviceId;
        Quantity = quantity;

        CreatedAt = DateTime.Now;
        CreatedBy = createdBy;
    }

    public int LineNumber { get; }

    public int PurchaseId { get; }
    public Purchase Purchase { get; private set; }

    public int DeviceId { get; }
    public Device Device { get; private set; }

    public int Quantity { get; }
}