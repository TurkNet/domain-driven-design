using MediatR;
using Microsoft.EntityFrameworkCore;
using Tn.Inventory.Application.Constants;
using Tn.Inventory.Application.Models;
using Tn.Inventory.Application.Purchases.Constants;
using Tn.Inventory.Infrastructure;

namespace Tn.Inventory.Application.Purchases.Commands;

public sealed record CompletePurchaseCommand(int PurchaseId, DateTime DeliveryDate, int TargetWarehouseId) : IRequest<ApiResponse>;

public sealed class CompletePurchaseCommandHandler : IRequestHandler<CompletePurchaseCommand, ApiResponse>
{
    private readonly IInventoryDbContext _context;

    public CompletePurchaseCommandHandler(IInventoryDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse> Handle(CompletePurchaseCommand request, CancellationToken cancellationToken)
    {
        var purchase = await _context.Purchases.FirstOrDefaultAsync(x => x.Id == request.PurchaseId && x.IsActive && !x.IsDeleted, cancellationToken);
        if (purchase is null)
            return new ApiResponse(false, ApiResponseType.RecordNotFound, ApplicationMessages.PurchaseNotFound);

        purchase.Complete(request.DeliveryDate, request.TargetWarehouseId, "hati");
        _context.Purchases.Update(purchase);

        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResponse(true, ApiResponseType.Success, ApplicationMessages.PurchaseCompleted);
    }
}