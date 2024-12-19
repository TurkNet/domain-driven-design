using MediatR;
using Microsoft.EntityFrameworkCore;
using Tn.Inventory.Application.Brands.Responses;
using Tn.Inventory.Application.Constants;
using Tn.Inventory.Application.Devices.Responses;
using Tn.Inventory.Application.Models;
using Tn.Inventory.Application.Purchases.Constants;
using Tn.Inventory.Application.Purchases.Responses;
using Tn.Inventory.Application.Suppliers.Responses;
using Tn.Inventory.Infrastructure;

namespace Tn.Inventory.Application.Purchases.Queries;

public sealed record PurchaseQuery(int? Id) : IRequest<ApiResponse<List<PurchaseResponse>>>;

public sealed class PurchaseQueryHandler : IRequestHandler<PurchaseQuery, ApiResponse<List<PurchaseResponse>>>
{
    private readonly IInventoryDbContext _context;

    public PurchaseQueryHandler(IInventoryDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<List<PurchaseResponse>>> Handle(PurchaseQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Purchases
            .Include(a => a.Supplier)
            .Include(a => a.PurchaseDetails)
            .ThenInclude(a => a.Device)
            .ThenInclude(a => a.Brand)
            .AsQueryable();

        if (request.Id.HasValue)
            query = query.Where(a => a.Id == request.Id);

        var result = await query.Select(a => new PurchaseResponse
        {
            Id = a.Id,
            Supplier = new SupplierResponse
            {
                Id = a.Supplier.Id,
                Code = a.Supplier.Code,
                Description = a.Supplier.Description,
                Name = a.Supplier.Name
            },
            DeliveryDate = a.DeliveryDate,
            OrderNumber = a.OrderNumber,
            PurchaseStatus = a.PurchaseStatus,
            TargetWarehouseId = a.TargetWarehouseId,
            PurchaseDetails = a.PurchaseDetails.Select(x => new PurchaseDetailResponse
            {
                Id = x.Id,
                Device = new DeviceResponse
                {
                    Id = x.Device.Id,
                    Code = x.Device.Code,
                    Description = x.Device.Description,
                    Name = x.Device.Name,
                    Brand = new BrandResponse
                    {
                        Id = x.Device.Brand.Id,
                        Name = x.Device.Brand.Name
                    },
                    Price = x.Device.Price
                },
                Quantity = x.Quantity,
                LineNumber = x.LineNumber
            }).ToList()
        }).ToListAsync(cancellationToken);

        return new ApiResponse<List<PurchaseResponse>>(true, ApiResponseType.Success, ApplicationMessages.Ok, result);
    }
}