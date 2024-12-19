using Tn.Inventory.Application.Purchases.Commands;

namespace Tn.Inventory.Application.Purchases.Requests;

public class CompletePurchaseRequest
{
    public CompletePurchaseRequest(int purchaseId, DateTime deliveryDate, int targetWarehouseId)
    {
        PurchaseId = purchaseId;
        DeliveryDate = deliveryDate;
        TargetWarehouseId = targetWarehouseId;
    }

    public int PurchaseId { get; }
    public DateTime DeliveryDate { get; }
    public int TargetWarehouseId { get; }

    public CompletePurchaseCommand ToCommand()
    {
        return new CompletePurchaseCommand(PurchaseId, DeliveryDate, TargetWarehouseId);
    }
}