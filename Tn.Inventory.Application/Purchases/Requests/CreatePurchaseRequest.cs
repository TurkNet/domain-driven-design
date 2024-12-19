using Tn.Inventory.Application.Purchases.Commands;

namespace Tn.Inventory.Application.Purchases.Requests;

public class CreatePurchaseRequest
{
    public CreatePurchaseRequest(int supplierId, string orderNumber, List<CreatePurchaseDetailRequest> details)
    {
        SupplierId = supplierId;
        OrderNumber = orderNumber;
        Details = details;
    }

    public int SupplierId { get; }
    public string OrderNumber { get; }
    public List<CreatePurchaseDetailRequest> Details { get; }

    public CreatePurchaseCommand ToCommand()
    {
        return new CreatePurchaseCommand(SupplierId, OrderNumber, Details);
    }
}