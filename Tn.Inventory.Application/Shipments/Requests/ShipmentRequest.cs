using Tn.Inventory.Application.Shipments.Queries;

namespace Tn.Inventory.Application.Shipments.Requests;

public class ShipmentRequest
{
    public ShipmentRequest()
    {
    }

    public ShipmentRequest(int? id)
    {
        Id = id;
    }

    public int? Id { get; set; }

    public ShipmentQuery ToQuery()
    {
        return new ShipmentQuery(Id);
    }
}