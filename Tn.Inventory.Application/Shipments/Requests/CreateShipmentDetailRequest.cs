namespace Tn.Inventory.Application.Shipments.Requests;

public class CreateShipmentDetailRequest
{
    public CreateShipmentDetailRequest(int lineNumber, int deviceId, int quantity)
    {
        LineNumber = lineNumber;
        DeviceId = deviceId;
        Quantity = quantity;
    }

    public int LineNumber { get; }
    public int DeviceId { get; }
    public int Quantity { get; }
}