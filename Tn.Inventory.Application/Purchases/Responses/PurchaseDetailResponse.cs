using Tn.Inventory.Application.Devices.Responses;

namespace Tn.Inventory.Application.Purchases.Responses;

public class PurchaseDetailResponse
{
    public int Id { get; set; }
    public int LineNumber { get; set; }
    public DeviceResponse Device { get; set; }
    public int Quantity { get; set; }
}