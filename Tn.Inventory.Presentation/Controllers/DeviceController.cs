using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tn.Inventory.Application.Devices.Requests;

namespace Tn.Inventory.Presentation.Controllers;

[ApiController]
[Route("device")]
public class DeviceController : ControllerBase
{
    private readonly IMediator _mediator;

    public DeviceController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeviceRequest().ToQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeviceRequest(id).ToQuery(), cancellationToken);
        return Ok(result);
    }
}