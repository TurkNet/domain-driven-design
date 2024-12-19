namespace Tn.Inventory.Application.Purchases.Requests;

public class CreatePurchaseDetailRequest
{
    public CreatePurchaseDetailRequest(int lineNumber, int deviceId, int quantity)
    {
        LineNumber = lineNumber;
        DeviceId = deviceId;
        Quantity = quantity;
    }

    public int LineNumber { get; }
    public int DeviceId { get; }
    public int Quantity { get; }
}