using CloudTicTacToe.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudTicTacToe.API.BaseClasses;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    protected readonly IMediator _mediator;

    public BaseApiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected ActionResult<T> HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        else if (result.IsNotFound)
        {
            return NotFound(result.ErrorMessage);
        }
        return BadRequest(result.ErrorMessage);
    }
}
