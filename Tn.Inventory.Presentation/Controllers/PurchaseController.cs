using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tn.Inventory.Application.Purchases.Requests;

namespace Tn.Inventory.Presentation.Controllers;

[ApiController]
[Route("purchase")]
public class PurchaseController : ControllerBase
{
    private readonly IMediator _mediator;

    public PurchaseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new PurchaseRequest().ToQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new PurchaseRequest(id).ToQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreatePurchaseRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request.ToCommand(), cancellationToken);
        return Ok(result);
    }

    [HttpPost("complete")]
    public async Task<IActionResult> Post([FromBody] CompletePurchaseRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request.ToCommand(), cancellationToken);
        return Ok(result);
    }
}