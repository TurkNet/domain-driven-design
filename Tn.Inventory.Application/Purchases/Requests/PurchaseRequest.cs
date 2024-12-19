using Tn.Inventory.Application.Purchases.Queries;

namespace Tn.Inventory.Application.Purchases.Requests;

public class PurchaseRequest
{
    public PurchaseRequest()
    {
    }

    public PurchaseRequest(int? id)
    {
        Id = id;
    }

    public int? Id { get; set; }

    public PurchaseQuery ToQuery()
    {
        return new PurchaseQuery(Id);
    }
}