using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tn.Inventory.Application.Brands.Requests;

namespace Tn.Inventory.Presentation.Controllers;

[ApiController]
[Route("brand")]
public class BrandController : ControllerBase
{
    private readonly IMediator _mediator;

    public BrandController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new BrandRequest().ToQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new BrandRequest(id).ToQuery(), cancellationToken);
        return Ok(result);
    }
}