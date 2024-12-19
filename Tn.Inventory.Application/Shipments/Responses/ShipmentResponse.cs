using System.Text.Json.Serialization;
using Tn.Inventory.Application.Warehouses.Responses;
using Tn.Inventory.Domain.Constants;
using Tn.Inventory.Domain.ValueObjects;

namespace Tn.Inventory.Application.Shipments.Responses;

public class ShipmentResponse
{
    public int Id { get; set; }
    public WarehouseResponse SourceWarehouse { get; set; }
    public WarehouseResponse TargetWarehouse { get; set; }
    public string Description { get; set; }

    [Newtonsoft.Json.JsonConverter(typeof(JsonStringEnumConverter))]
    public ShipmentStatus ShipmentStatus { get; set; }

    public DateTime? CompletedDate { get; set; }
    public Address ShippingAddress { get; set; }
    public string ReceiverName { get; set; }
    public string ReceiverPhoneNumber { get; set; }

    public List<ShipmentDetailResponse> ShipmentDetails { get; set; }
}