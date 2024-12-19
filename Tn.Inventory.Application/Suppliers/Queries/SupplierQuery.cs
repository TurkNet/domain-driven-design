using MediatR;
using Microsoft.EntityFrameworkCore;
using Tn.Inventory.Application.Constants;
using Tn.Inventory.Application.Models;
using Tn.Inventory.Application.Suppliers.Constants;
using Tn.Inventory.Application.Suppliers.Responses;
using Tn.Inventory.Infrastructure;

namespace Tn.Inventory.Application.Suppliers.Queries;

public sealed record SupplierQuery(int? Id) : IRequest<ApiResponse<List<SupplierResponse>>>;

public sealed class SupplierQueryHandler : IRequestHandler<SupplierQuery, ApiResponse<List<SupplierResponse>>>
{
    private readonly IInventoryDbContext _context;

    public SupplierQueryHandler(IInventoryDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<List<SupplierResponse>>> Handle(SupplierQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Suppliers.AsQueryable();

        if (request.Id.HasValue)
            query = query.Where(a => a.Id == request.Id);

        var result = await query.Select(a => new SupplierResponse
        {
            Id = a.Id,
            Code = a.Code,
            Name = a.Name,
            Description = a.Description
        }).ToListAsync(cancellationToken);

        return new ApiResponse<List<SupplierResponse>>(true, ApiResponseType.Success, ApplicationMessages.Ok, result);
    }
}