using Tn.Inventory.Application.Warehouses.Queries;

namespace Tn.Inventory.Application.Warehouses.Requests;

public class WarehouseRequest
{
    public WarehouseRequest()
    {
    }

    public WarehouseRequest(int? id)
    {
        Id = id;
    }

    public int? Id { get; set; }

    public WarehouseQuery ToQuery()
    {
        return new WarehouseQuery(Id);
    }
}