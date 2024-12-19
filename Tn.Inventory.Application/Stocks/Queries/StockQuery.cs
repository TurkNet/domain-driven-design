using MediatR;
using Microsoft.EntityFrameworkCore;
using Tn.Inventory.Application.Brands.Responses;
using Tn.Inventory.Application.Constants;
using Tn.Inventory.Application.Devices.Responses;
using Tn.Inventory.Application.Models;
using Tn.Inventory.Application.Stocks.Constants;
using Tn.Inventory.Application.Stocks.Responses;
using Tn.Inventory.Application.Suppliers.Responses;
using Tn.Inventory.Application.Warehouses.Responses;
using Tn.Inventory.Infrastructure;

namespace Tn.Inventory.Application.Stocks.Queries;

public sealed record StockQuery(int? Id) : IRequest<ApiResponse<List<StockResponse>>>;

public sealed class StockQueryHandler : IRequestHandler<StockQuery, ApiResponse<List<StockResponse>>>
{
    private readonly IInventoryDbContext _context;

    public StockQueryHandler(IInventoryDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<List<StockResponse>>> Handle(StockQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Stocks
            .Include(a => a.Warehouse)
            .ThenInclude(a => a.Supplier)
            .Include(a => a.Device)
            .ThenInclude(a => a.Brand)
            .AsQueryable();

        if (request.Id.HasValue)
            query = query.Where(a => a.Id == request.Id);

        var result = await query.Select(a => new StockResponse
        {
            Id = a.Id,
            Device = new DeviceResponse
            {
                Id = a.Device.Id,
                Name = a.Device.Name,
                Brand = new BrandResponse
                {
                    Id = a.Device.Brand.Id,
                    Name = a.Device.Brand.Name
                },
                Code = a.Device.Code,
                Description = a.Device.Description,
                Price = a.Device.Price
            },
            Warehouse = new WarehouseResponse
            {
                Id = a.Warehouse.Id,
                Name = a.Warehouse.Name,
                Supplier = new SupplierResponse
                {
                    Id = a.Warehouse.Supplier.Id,
                    Code = a.Warehouse.Supplier.Code,
                    Description = a.Warehouse.Supplier.Description,
                    Name = a.Warehouse.Supplier.Name
                },
                Address = a.Warehouse.Address,
                SupplierId = a.Warehouse.SupplierId
            },
            Quantity = a.Quantity
        }).ToListAsync(cancellationToken);

        return new ApiResponse<List<StockResponse>>(true, ApiResponseType.Success, ApplicationMessages.Ok, result);
    }
}