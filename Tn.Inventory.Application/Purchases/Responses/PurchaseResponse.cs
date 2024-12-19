using System.Text.Json.Serialization;
using Tn.Inventory.Application.Suppliers.Responses;
using Tn.Inventory.Domain.Constants;

namespace Tn.Inventory.Application.Purchases.Responses;

public class PurchaseResponse
{
    public int Id { get; set; }

    [Newtonsoft.Json.JsonConverter(typeof(JsonStringEnumConverter))]
    public PurchaseStatus PurchaseStatus { get; set; }

    public SupplierResponse Supplier { get; set; }
    public string OrderNumber { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public int? TargetWarehouseId { get; set; }
    public List<PurchaseDetailResponse> PurchaseDetails { get; set; }
}