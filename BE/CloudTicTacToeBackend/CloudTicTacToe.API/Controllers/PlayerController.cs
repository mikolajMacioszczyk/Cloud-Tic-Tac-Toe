using CloudTicTacToe.Application.Commands.Players.RegisterPlayer;
using CloudTicTacToe.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CloudTicTacToe.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlayerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        public async Task<ActionResult<Player>> RegisterPlayer([FromBody] RegisterPlayerCommand command) =>
            Ok(await _mediator.Send(command));
    }
}
