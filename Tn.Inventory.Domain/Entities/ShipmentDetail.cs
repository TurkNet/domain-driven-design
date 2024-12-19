using Tn.Inventory.Domain.Aggregates;
using Tn.Inventory.Domain.Primitives;

namespace Tn.Inventory.Domain.Entities;

public sealed class ShipmentDetail : Entity
{
    public ShipmentDetail(int lineNumber, int shipmentId, int deviceId, int quantity, string createdBy)
    {
        LineNumber = lineNumber;
        ShipmentId = shipmentId;
        DeviceId = deviceId;
        Quantity = quantity;

        CreatedAt = DateTime.Now;
        CreatedBy = createdBy;
    }

    public int LineNumber { get; }

    public int ShipmentId { get; }
    public Shipment Shipment { get; private set; }

    public int DeviceId { get; }
    public Device Device { get; private set; }

    public int Quantity { get; }
}