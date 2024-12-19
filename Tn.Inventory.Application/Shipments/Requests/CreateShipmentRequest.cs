using Tn.Inventory.Application.Shipments.Commands;
using Tn.Inventory.Domain.ValueObjects;

namespace Tn.Inventory.Application.Shipments.Requests;

public class CreateShipmentRequest
{
    public CreateShipmentRequest(int sourceWarehouseId, int targetWarehouseId, string description, Address shippingAddress, string receiverName, string receiverPhoneNumber, List<CreateShipmentDetailRequest> details)
    {
        SourceWarehouseId = sourceWarehouseId;
        TargetWarehouseId = targetWarehouseId;
        Description = description;
        ShippingAddress = shippingAddress;
        ReceiverName = receiverName;
        ReceiverPhoneNumber = receiverPhoneNumber;
        Details = details;
    }

    public int SourceWarehouseId { get; }
    public int TargetWarehouseId { get; }
    public string Description { get; }
    public Address ShippingAddress { get; }
    public string ReceiverName { get; }
    public string ReceiverPhoneNumber { get; }
    public List<CreateShipmentDetailRequest> Details { get; private set; }

    public CreateShipmentCommand ToCommand()
    {
        return new CreateShipmentCommand(SourceWarehouseId, TargetWarehouseId, Description, ShippingAddress, ReceiverName, ReceiverPhoneNumber, Details);
    }
}