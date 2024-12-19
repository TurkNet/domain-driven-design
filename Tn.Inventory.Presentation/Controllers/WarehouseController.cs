using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tn.Inventory.Application.Warehouses.Requests;

namespace Tn.Inventory.Presentation.Controllers;

[ApiController]
[Route("warehouse")]
public class WarehouseController : ControllerBase
{
    private readonly IMediator _mediator;

    public WarehouseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new WarehouseRequest().ToQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new WarehouseRequest(id).ToQuery(), cancellationToken);
        return Ok(result);
    }
}