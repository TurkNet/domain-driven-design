using Tn.Inventory.Domain.Entities;
using Tn.Inventory.Domain.Primitives;

namespace Tn.Inventory.Domain.Aggregates;

public sealed class Stock : Aggregate
{
    public Stock(int warehouseId, int deviceId, decimal quantity, string createdBy)
    {
        WarehouseId = warehouseId;
        DeviceId = deviceId;
        Quantity = quantity;

        CreatedAt = DateTime.Now;
        CreatedBy = createdBy;
    }

    public int WarehouseId { get; }
    public Warehouse Warehouse { get; private set; }

    public int DeviceId { get; }
    public Device Device { get; private set; }

    public decimal Quantity { get; private set; }

    public void ReduceStock(int demand, string modifiedBy)
    {
        Quantity -= demand;

        LastModifiedAt = DateTime.Now;
        LastModifiedBy = modifiedBy;
    }

    public void IncreaseStock(int demand, string modifiedBy)
    {
        Quantity += demand;

        LastModifiedAt = DateTime.Now;
        LastModifiedBy = modifiedBy;
    }
}