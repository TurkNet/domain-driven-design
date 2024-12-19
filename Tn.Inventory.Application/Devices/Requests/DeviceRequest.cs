using Tn.Inventory.Application.Devices.Queries;

namespace Tn.Inventory.Application.Devices.Requests;

public class DeviceRequest
{
    public DeviceRequest()
    {
    }

    public DeviceRequest(int? id)
    {
        Id = id;
    }

    public int? Id { get; set; }

    public DeviceQuery ToQuery()
    {
        return new DeviceQuery(Id);
    }
}