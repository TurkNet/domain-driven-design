using MediatR;
using Microsoft.EntityFrameworkCore;
using Tn.Inventory.Application.Constants;
using Tn.Inventory.Application.Models;
using Tn.Inventory.Application.Purchases.Constants;
using Tn.Inventory.Application.Purchases.Requests;
using Tn.Inventory.Domain.Aggregates;
using Tn.Inventory.Domain.Entities;
using Tn.Inventory.Infrastructure;

namespace Tn.Inventory.Application.Purchases.Commands;

public sealed record CreatePurchaseCommand(int SupplierId, string OrderNumber, List<CreatePurchaseDetailRequest> Details) : IRequest<ApiResponse>;

public sealed class CreatePurchaseCommandHandler : IRequestHandler<CreatePurchaseCommand, ApiResponse>
{
    private readonly IInventoryDbContext _context;

    public CreatePurchaseCommandHandler(IInventoryDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse> Handle(CreatePurchaseCommand request, CancellationToken cancellationToken)
    {
        var exists = await _context.Purchases.AnyAsync(x => x.OrderNumber == request.OrderNumber && x.IsActive && !x.IsDeleted, cancellationToken);
        if (exists)
            return new ApiResponse(false, ApiResponseType.BadRequest, ApplicationMessages.PurchaseAlreadyExists);

        var purchase = new Purchase(request.SupplierId, request.OrderNumber, "hati");
        await _context.Purchases.AddAsync(purchase, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var details = request.Details.Select(a => new PurchaseDetail(a.LineNumber, purchase.Id, a.DeviceId, a.Quantity, "hati"));
        await _context.PurchaseDetails.AddRangeAsync(details, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResponse(true, ApiResponseType.Success, ApplicationMessages.PurchaseCreated);
    }
}