using CloudTicTacToe.API.BaseClasses;
using CloudTicTacToe.Application.Commands.Players.RegisterOrLoginPlayer;
using CloudTicTacToe.Application.Queries.Players.GetAllPlayers;
using CloudTicTacToe.Application.Queries.Players.GetPlayerById;
using CloudTicTacToe.Application.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CloudTicTacToe.API.Controllers
{
    public class PlayerController : BaseApiController
    {
        public PlayerController(IMediator mediator) : base(mediator)
        { }

        [HttpGet("{Id}")]
        public async Task<ActionResult<PlayerResult>> GetAllPlayers([FromQuery] GetPlayerByIdQuery query) =>
            HandleResult(await _mediator.Send(query));

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<PlayerResult>>> GetAllPlayers([FromQuery] GetAllPlayersQuery query) =>
            HandleResult(await _mediator.Send(query));

        [HttpPost()]
        public async Task<ActionResult<PlayerResult>> RegisterOrLoginPlayer([FromBody] RegisterOrLoginPlayerCommand command) =>
            HandleResult(await _mediator.Send(command));
    }
}
