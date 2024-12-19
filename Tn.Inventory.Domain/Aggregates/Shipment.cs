using Tn.Inventory.Domain.Constants;
using Tn.Inventory.Domain.Entities;
using Tn.Inventory.Domain.Events;
using Tn.Inventory.Domain.Primitives;
using Tn.Inventory.Domain.ValueObjects;

namespace Tn.Inventory.Domain.Aggregates;

public sealed class Shipment : Aggregate
{
    public Shipment(int sourceWarehouseId, int targetWarehouseId, string description, Address shippingAddress, string receiverName, string receiverPhoneNumber, string createdBy)
    {
        SourceWarehouseId = sourceWarehouseId;
        TargetWarehouseId = targetWarehouseId;
        Description = description;
        ShippingAddress = shippingAddress;
        ReceiverName = receiverName;
        ReceiverPhoneNumber = receiverPhoneNumber;
        ShipmentStatus = ShipmentStatus.New;
        ShipmentDetails = new List<ShipmentDetail>();

        CreatedAt = DateTime.Now;
        CreatedBy = createdBy;
    }

    public int SourceWarehouseId { get; }
    public Warehouse SourceWarehouse { get; private set; }

    public int TargetWarehouseId { get; }
    public Warehouse TargetWarehouse { get; private set; }

    public string Description { get; }

    public ShipmentStatus ShipmentStatus { get; private set; }

    public DateTime? CompletedDate { get; private set; }

    public Address ShippingAddress { get; }

    public string ReceiverName { get; }
    public string ReceiverPhoneNumber { get; }

    public IEnumerable<ShipmentDetail> ShipmentDetails { get; }

    public void Complete(DateTime completedDate, string modifiedBy)
    {
        ShipmentStatus = ShipmentStatus.Completed;
        CompletedDate = completedDate;

        LastModifiedAt = DateTime.Now;
        LastModifiedBy = modifiedBy;

        RaiseEvent(new ShipmentCompletedEvent(Id));
    }
}