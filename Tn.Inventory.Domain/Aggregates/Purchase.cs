using Tn.Inventory.Domain.Constants;
using Tn.Inventory.Domain.Entities;
using Tn.Inventory.Domain.Events;
using Tn.Inventory.Domain.Primitives;

namespace Tn.Inventory.Domain.Aggregates;

public sealed class Purchase : Aggregate
{
    public Purchase(int supplierId, string orderNumber, string createdBy)
    {
        SupplierId = supplierId;
        PurchaseStatus = PurchaseStatus.New;
        OrderNumber = orderNumber;
        PurchaseDetails = new List<PurchaseDetail>();

        CreatedAt = DateTime.Now;
        CreatedBy = createdBy;
    }

    public int SupplierId { get; }
    public Supplier Supplier { get; private set; }
    public PurchaseStatus PurchaseStatus { get; private set; }
    public string OrderNumber { get; }
    public DateTime? DeliveryDate { get; private set; }
    public int? TargetWarehouseId { get; private set; }
    public IEnumerable<PurchaseDetail> PurchaseDetails { get; }

    public void Complete(DateTime deliveryDate, int targetWarehouseId, string modifiedBy)
    {
        PurchaseStatus = PurchaseStatus.Completed;
        DeliveryDate = deliveryDate;
        TargetWarehouseId = targetWarehouseId;

        LastModifiedAt = DateTime.Now;
        LastModifiedBy = modifiedBy;

        RaiseEvent(new PurchaseCompletedEvent(Id));
    }
}