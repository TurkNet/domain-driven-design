using Tn.Inventory.Application.Suppliers.Responses;
using Tn.Inventory.Domain.ValueObjects;

namespace Tn.Inventory.Application.Warehouses.Responses;

public class WarehouseResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SupplierId { get; set; }
    public SupplierResponse Supplier { get; set; }
    public Address Address { get; set; }
}