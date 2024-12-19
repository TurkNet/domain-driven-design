using Tn.Inventory.Application.Brands.Responses;

namespace Tn.Inventory.Application.Devices.Responses;

public class DeviceResponse
{
    public int Id { get; set; }
    public BrandResponse Brand { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}