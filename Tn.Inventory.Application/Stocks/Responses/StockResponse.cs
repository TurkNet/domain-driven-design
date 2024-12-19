using Tn.Inventory.Application.Devices.Responses;
using Tn.Inventory.Application.Warehouses.Responses;

namespace Tn.Inventory.Application.Stocks.Responses;

public class StockResponse
{
    public int Id { get; set; }
    public WarehouseResponse Warehouse { get; set; }
    public DeviceResponse Device { get; set; }
    public decimal Quantity { get; set; }
}