using MediatR;
using Tn.Inventory.Application.Constants;
using Tn.Inventory.Application.Models;
using Tn.Inventory.Application.Shipments.Constants;
using Tn.Inventory.Application.Shipments.Requests;
using Tn.Inventory.Domain.Aggregates;
using Tn.Inventory.Domain.Entities;
using Tn.Inventory.Domain.ValueObjects;
using Tn.Inventory.Infrastructure;

namespace Tn.Inventory.Application.Shipments.Commands;

public sealed record CreateShipmentCommand(int SourceWarehouseId, int TargetWarehouseId, string Description, Address ShippingAddress, string ReceiverName, string ReceiverPhoneNumber, List<CreateShipmentDetailRequest> Details) : IRequest<ApiResponse>;

public sealed class CreateShipmentCommandHandler : IRequestHandler<CreateShipmentCommand, ApiResponse>
{
    private readonly IInventoryDbContext _context;

    public CreateShipmentCommandHandler(IInventoryDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse> Handle(CreateShipmentCommand request, CancellationToken cancellationToken)
    {
        var shipment = new Shipment(request.SourceWarehouseId, request.TargetWarehouseId, request.Description, request.ShippingAddress, request.ReceiverName, request.ReceiverPhoneNumber, "hati");
        await _context.Shipments.AddAsync(shipment, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var details = request.Details.Select(a => new ShipmentDetail(a.LineNumber, shipment.Id, a.DeviceId, a.Quantity, "hati"));
        await _context.ShipmentDetails.AddRangeAsync(details, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResponse(true, ApiResponseType.Success, ApplicationMessages.ShipmentCreated);
    }
}