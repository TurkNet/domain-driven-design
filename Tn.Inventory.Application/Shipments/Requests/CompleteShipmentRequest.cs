using Tn.Inventory.Application.Shipments.Commands;

namespace Tn.Inventory.Application.Shipments.Requests;

public class CompleteShipmentRequest
{
    public CompleteShipmentRequest(int shipmentId, DateTime deliveryDate)
    {
        ShipmentId = shipmentId;
        DeliveryDate = deliveryDate;
    }

    public int ShipmentId { get; }
    public DateTime DeliveryDate { get; }

    public CompleteShipmentCommand ToCommand()
    {
        return new CompleteShipmentCommand(ShipmentId, DeliveryDate);
    }
}