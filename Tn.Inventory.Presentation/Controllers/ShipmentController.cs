using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tn.Inventory.Application.Shipments.Requests;

namespace Tn.Inventory.Presentation.Controllers;

[ApiController]
[Route("shipment")]
public class ShipmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public ShipmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ShipmentRequest().ToQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ShipmentRequest(id).ToQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateShipmentRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request.ToCommand(), cancellationToken);
        return Ok(result);
    }

    [HttpPost("complete")]
    public async Task<IActionResult> Post([FromBody] CompleteShipmentRequest request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request.ToCommand(), cancellationToken);
        return Ok(result);
    }
}