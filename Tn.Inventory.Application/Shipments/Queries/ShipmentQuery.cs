using MediatR;
using Microsoft.EntityFrameworkCore;
using Tn.Inventory.Application.Brands.Responses;
using Tn.Inventory.Application.Constants;
using Tn.Inventory.Application.Devices.Responses;
using Tn.Inventory.Application.Models;
using Tn.Inventory.Application.Shipments.Constants;
using Tn.Inventory.Application.Shipments.Responses;
using Tn.Inventory.Application.Suppliers.Responses;
using Tn.Inventory.Application.Warehouses.Responses;
using Tn.Inventory.Infrastructure;

namespace Tn.Inventory.Application.Shipments.Queries;

public sealed record ShipmentQuery(int? Id) : IRequest<ApiResponse<List<ShipmentResponse>>>;

public sealed class ShipmentQueryHandler : IRequestHandler<ShipmentQuery, ApiResponse<List<ShipmentResponse>>>
{
    private readonly IInventoryDbContext _context;

    public ShipmentQueryHandler(IInventoryDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResponse<List<ShipmentResponse>>> Handle(ShipmentQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Shipments
            .Include(a => a.SourceWarehouse)
                .ThenInclude(a => a.Supplier)
            .Include(a => a.TargetWarehouse)
                .ThenInclude(a => a.Supplier)
            .Include(a => a.ShipmentDetails)
                .ThenInclude(a => a.Device)
                .ThenInclude(a => a.Brand)
            .AsQueryable();

        if (request.Id.HasValue)
            query = query.Where(a => a.Id == request.Id);

        var result = query.Select(a  => new ShipmentResponse
        {
            Id = a.Id,
            CompletedDate = a.CompletedDate,
            Description = a.Description,
            ShipmentStatus = a.ShipmentStatus,
            SourceWarehouse = new WarehouseResponse
            {
                Id = a.SourceWarehouse.Id,
                Address = a.SourceWarehouse.Address,
                Name = a.SourceWarehouse.Name,
                Supplier = new SupplierResponse
                {
                    Id = a.SourceWarehouse.Supplier.Id,
                    Code = a.SourceWarehouse.Supplier.Code,
                    Description = a.SourceWarehouse.Supplier.Description,
                    Name = a.SourceWarehouse.Supplier.Name
                },
                SupplierId = a.SourceWarehouse.SupplierId
            },
            ShippingAddress = a.ShippingAddress,
            ReceiverName = a.ReceiverName,
            ReceiverPhoneNumber = a.ReceiverPhoneNumber,
            TargetWarehouse = new WarehouseResponse
            {
                Id = a.TargetWarehouse.Id,
                Address = a.TargetWarehouse.Address,
                Name = a.TargetWarehouse.Name,
                Supplier = new SupplierResponse
                {
                    Id = a.TargetWarehouse.Supplier.Id,
                    Code = a.TargetWarehouse.Supplier.Code,
                    Description = a.TargetWarehouse.Supplier.Description,
                    Name = a.TargetWarehouse.Supplier.Name
                },
                SupplierId = a.TargetWarehouse.SupplierId
            },
            ShipmentDetails = a.ShipmentDetails.Select(x => new ShipmentDetailResponse
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
        }).ToList();

        return new ApiResponse<List<ShipmentResponse>>(true, ApiResponseType.Success, ApplicationMessages.Ok, result);
    }
}