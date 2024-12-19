using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tn.Inventory.Application.Suppliers.Requests;

namespace Tn.Inventory.Presentation.Controllers;

[ApiController]
[Route("supplier")]
public class SupplierController : ControllerBase
{
    private readonly IMediator _mediator;

    public SupplierController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SupplierRequest().ToQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new SupplierRequest(id).ToQuery(), cancellationToken);
        return Ok(result);
    }
}