using Tn.Inventory.Domain.Aggregates;
using Tn.Inventory.Domain.Primitives;
using Tn.Inventory.Domain.ValueObjects;

namespace Tn.Inventory.Domain.Entities;

public sealed class Warehouse : Entity
{
    public Warehouse(string name, int supplierId, Address address, string createdBy)
    {
        Name = name;
        SupplierId = supplierId;
        Address = address;
        Stocks = new List<Stock>();
        SourceShipments = new List<Shipment>();
        TargetShipments = new List<Shipment>();

        CreatedAt = DateTime.Now;
        CreatedBy = createdBy;
    }

    public string Name { get; }
    public int SupplierId { get; }
    public Supplier Supplier { get; private set; }
    public Address Address { get; }

    public IEnumerable<Stock> Stocks { get; }
    public IEnumerable<Shipment> SourceShipments { get; }
    public IEnumerable<Shipment> TargetShipments { get; }
}