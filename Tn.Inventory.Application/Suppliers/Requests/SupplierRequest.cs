using Tn.Inventory.Application.Suppliers.Queries;

namespace Tn.Inventory.Application.Suppliers.Requests;

public class SupplierRequest
{
    public SupplierRequest()
    {
    }

    public SupplierRequest(int? id)
    {
        Id = id;
    }

    public int? Id { get; set; }

    public SupplierQuery ToQuery()
    {
        return new SupplierQuery(Id);
    }
}