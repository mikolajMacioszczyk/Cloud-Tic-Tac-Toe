using CloudTicTacToe.API.BaseClasses;
using CloudTicTacToe.Application.Commands.Players.RegisterPlayer;
using CloudTicTacToe.Application.Queries.Players.GetAllPlayers;
using CloudTicTacToe.Application.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CloudTicTacToe.API.Controllers
{
    public class PlayerController : BaseApiController
    {
        public PlayerController(IMediator mediator) : base(mediator)
        {}

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<PlayerResult>>> GetAllPlayers([FromQuery] GetAllPlayersCommand command) =>
            HandleResult(await _mediator.Send(command));

        [HttpPost()]
        public async Task<ActionResult<PlayerResult>> RegisterPlayer([FromBody] RegisterPlayerCommand command) =>
            HandleResult(await _mediator.Send(command));
    }
}
