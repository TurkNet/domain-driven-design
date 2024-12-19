using MediatR;
using Microsoft.EntityFrameworkCore;
using Tn.Inventory.Application.Brands.Responses;
using Tn.Inventory.Application.Constants;
using Tn.Inventory.Application.Devices.Constants;
using Tn.Inventory.Application.Devices.Responses;
using Tn.Inventory.Application.Models;
using Tn.Inventory.Infrastructure;

namespace Tn.Inventory.Application.Devices.Queries;

public sealed record DeviceQuery(int? Id) : IRequest<ApiResponse<List<DeviceResponse>>>;

public sealed class DeviceQueryHandler : IRequestHandler<DeviceQuery, ApiResponse<List<DeviceResponse>>>
{
    private readonly IInventoryDbContext _context;

    public DeviceQueryHandler(IInventoryDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<List<DeviceResponse>>> Handle(DeviceQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Devices.Include(a => a.Brand).AsQueryable();

        if (request.Id.HasValue)
            query = query.Where(a => a.Id == request.Id);

        var result = await query.Select(a => new DeviceResponse
        {
            Id = a.Id,
            Brand = new BrandResponse
            {
                Id = a.Brand.Id,
                Name = a.Brand.Name
            },
            Code = a.Code,
            Name = a.Name,
            Description = a.Description,
            Price = a.Price
        }).ToListAsync(cancellationToken);

        return new ApiResponse<List<DeviceResponse>>(true, ApiResponseType.Success, ApplicationMessages.Ok, result);
    }
}