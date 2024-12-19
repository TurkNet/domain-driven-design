using Tn.Inventory.Domain.Aggregates;
using Tn.Inventory.Domain.Primitives;

namespace Tn.Inventory.Domain.Entities;

public sealed class Supplier : Entity
{
    public Supplier(string code, string name, string description, string createdBy)
    {
        Code = code;
        Name = name;
        Description = description;
        Warehouses = new List<Warehouse>();
        Purchases = new List<Purchase>();

        CreatedAt = DateTime.Now;
        CreatedBy = createdBy;
    }

    public string Code { get; }
    public string Name { get; }
    public string Description { get; private set; }
    public IEnumerable<Warehouse> Warehouses { get; }
    public IEnumerable<Purchase> Purchases { get; }
}