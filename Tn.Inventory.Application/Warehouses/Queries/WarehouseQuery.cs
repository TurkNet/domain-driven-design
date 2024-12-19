using MediatR;
using Microsoft.EntityFrameworkCore;
using Tn.Inventory.Application.Constants;
using Tn.Inventory.Application.Models;
using Tn.Inventory.Application.Suppliers.Responses;
using Tn.Inventory.Application.Warehouses.Constants;
using Tn.Inventory.Application.Warehouses.Responses;
using Tn.Inventory.Infrastructure;

namespace Tn.Inventory.Application.Warehouses.Queries;

public sealed record WarehouseQuery(int? Id) : IRequest<ApiResponse<List<WarehouseResponse>>>;

public sealed class WarehouseQueryHandler : IRequestHandler<WarehouseQuery, ApiResponse<List<WarehouseResponse>>>
{
    private readonly IInventoryDbContext _context;

    public WarehouseQueryHandler(IInventoryDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<List<WarehouseResponse>>> Handle(WarehouseQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Warehouses.Include(a => a.Supplier).AsQueryable();

        if (request.Id.HasValue)
            query = query.Where(a => a.Id == request.Id);

        var result = await query.Select(a => new WarehouseResponse
        {
            Id = a.Id,
            Address = a.Address,
            Name = a.Name,
            Supplier = new SupplierResponse
            {
                Id = a.Supplier.Id,
                Code = a.Supplier.Code,
                Description = a.Supplier.Description,
                Name = a.Supplier.Name
            },
            SupplierId = a.SupplierId
        }).ToListAsync(cancellationToken);

        return new ApiResponse<List<WarehouseResponse>>(true, ApiResponseType.Success, ApplicationMessages.Ok, result);
    }
}