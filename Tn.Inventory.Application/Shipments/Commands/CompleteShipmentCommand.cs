using MediatR;
using Microsoft.EntityFrameworkCore;
using Tn.Inventory.Application.Constants;
using Tn.Inventory.Application.Models;
using Tn.Inventory.Application.Shipments.Constants;
using Tn.Inventory.Infrastructure;

namespace Tn.Inventory.Application.Shipments.Commands;

public sealed record CompleteShipmentCommand(int ShipmentId, DateTime DeliveryDate) : IRequest<ApiResponse>;

public sealed class CompleteShipmentCommandHandler : IRequestHandler<CompleteShipmentCommand, ApiResponse>
{
    private readonly IInventoryDbContext _context;

    public CompleteShipmentCommandHandler(IInventoryDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse> Handle(CompleteShipmentCommand request, CancellationToken cancellationToken)
    {
        var shipment = await _context.Shipments.FirstOrDefaultAsync(x => x.Id == request.ShipmentId && x.IsActive && !x.IsDeleted, cancellationToken);
        if (shipment is null)
            return new ApiResponse(false, ApiResponseType.RecordNotFound, ApplicationMessages.ShipmentNotFound);

        shipment.Complete(request.DeliveryDate, "hati");
        _context.Shipments.Update(shipment);

        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResponse(true, ApiResponseType.Success, ApplicationMessages.ShipmentCompleted);
    }
}