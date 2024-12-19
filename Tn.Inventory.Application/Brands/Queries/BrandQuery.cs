using MediatR;
using Microsoft.EntityFrameworkCore;
using Tn.Inventory.Application.Brands.Constants;
using Tn.Inventory.Application.Brands.Responses;
using Tn.Inventory.Application.Constants;
using Tn.Inventory.Application.Models;
using Tn.Inventory.Infrastructure;

namespace Tn.Inventory.Application.Brands.Queries;

public sealed record BrandQuery(int? Id) : IRequest<ApiResponse<List<BrandResponse>>>;

public sealed class BrandQueryHandler : IRequestHandler<BrandQuery, ApiResponse<List<BrandResponse>>>
{
    private readonly IInventoryDbContext _context;

    public BrandQueryHandler(IInventoryDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<List<BrandResponse>>> Handle(BrandQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Brands.AsQueryable();

        if (request.Id.HasValue)
            query = query.Where(a => a.Id == request.Id);

        var result = await query.Select(a => new BrandResponse
        {
            Id = a.Id,
            Name = a.Name
        }).ToListAsync(cancellationToken);

        return new ApiResponse<List<BrandResponse>>(true, ApiResponseType.Success, ApplicationMessages.Ok, result);
    }
}