using Tn.Inventory.Application.Brands.Queries;

namespace Tn.Inventory.Application.Brands.Requests;

public class BrandRequest
{
    public BrandRequest()
    {
    }

    public BrandRequest(int? id)
    {
        Id = id;
    }

    public int? Id { get; set; }

    public BrandQuery ToQuery()
    {
        return new BrandQuery(Id);
    }
}