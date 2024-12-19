using Tn.Inventory.Application.Devices.Responses;

namespace Tn.Inventory.Application.Shipments.Responses;

public class ShipmentDetailResponse
{
    public int Id { get; set; }
    public int LineNumber { get; set; }
    public DeviceResponse Device { get; set; }
    public int Quantity { get; set; }
}