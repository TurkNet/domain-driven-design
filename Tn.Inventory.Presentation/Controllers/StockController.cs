using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tn.Inventory.Application.Stocks.Requests;

namespace Tn.Inventory.Presentation.Controllers;

[ApiController]
[Route("stock")]
public class StockController : ControllerBase
{
    private readonly IMediator _mediator;

    public StockController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new StockRequest().ToQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new StockRequest(id).ToQuery(), cancellationToken);
        return Ok(result);
    }
}