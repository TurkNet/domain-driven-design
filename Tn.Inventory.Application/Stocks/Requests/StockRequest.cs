using Tn.Inventory.Application.Stocks.Queries;

namespace Tn.Inventory.Application.Stocks.Requests;

public class StockRequest
{
    public StockRequest()
    {
    }

    public StockRequest(int? id)
    {
        Id = id;
    }

    public int? Id { get; set; }

    public StockQuery ToQuery()
    {
        return new StockQuery(Id);
    }
}