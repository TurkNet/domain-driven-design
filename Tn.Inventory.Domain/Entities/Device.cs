using Tn.Inventory.Domain.Aggregates;
using Tn.Inventory.Domain.Primitives;

namespace Tn.Inventory.Domain.Entities;

public sealed class Device : Entity
{
    public Device(int brandId, string code, string name, string description, decimal price, string createdBy)
    {
        BrandId = brandId;
        Code = code;
        Name = name;
        Description = description;
        Price = price;
        PurchaseDetails = new List<PurchaseDetail>();
        Stocks = new List<Stock>();
        ShipmentDetails = new List<ShipmentDetail>();

        CreatedAt = DateTime.Now;
        CreatedBy = createdBy;
    }

    public int BrandId { get; }
    public Brand Brand { get; private set; }
    public string Code { get; }
    public string Name { get; }
    public string Description { get; }
    public decimal Price { get; }

    public IEnumerable<PurchaseDetail> PurchaseDetails { get; }
    public IEnumerable<Stock> Stocks { get; }
    public IEnumerable<ShipmentDetail> ShipmentDetails { get; }
}