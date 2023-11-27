using CloudTicTacToe.API.BaseClasses;
using CloudTicTacToe.Application.Commands.Players.GetAllPlayers;
using CloudTicTacToe.Application.Commands.Players.RegisterPlayer;
using CloudTicTacToe.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CloudTicTacToe.API.Controllers
{
    public class PlayerController : BaseApiController
    {
        public PlayerController(IMediator mediator) : base(mediator)
        {}

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Player>>> GetAllPlayers([FromBody] GetAllPlayersCommand command) =>
            HandleResult(await _mediator.Send(command));

        [HttpPost()]
        public async Task<ActionResult<Player>> RegisterPlayer([FromBody] RegisterPlayerCommand command) =>
            HandleResult(await _mediator.Send(command));
    }
}
