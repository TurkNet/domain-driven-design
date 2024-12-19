using Tn.Inventory.Domain.Primitives;

namespace Tn.Inventory.Domain.Entities;

public sealed class Brand : Entity
{
    public Brand(string name, string createdBy)
    {
        Name = name;
        Devices = new List<Device>();

        CreatedAt = DateTime.Now;
        CreatedBy = createdBy;
    }

    public string Name { get; }
    public IEnumerable<Device> Devices { get; }
}