using CloudTicTacToe.API.BaseClasses;
using CloudTicTacToe.Application.Commands.Games.InitializeGameWithComputer;
using CloudTicTacToe.Application.Commands.Games.PlayTurn;
using CloudTicTacToe.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CloudTicTacToe.API.Controllers
{
    public class GameController : BaseApiController
    {
        public GameController(IMediator mediator) : base(mediator)
        {}

        [HttpPost()]
        public async Task<ActionResult<GameBoard>> InitializeGameWithComputer([FromBody] InitializeGameWithComputerCommand command) =>
            HandleResult(await _mediator.Send(command));

        [HttpPut("{Id}/actions/play")]
        public async Task<ActionResult<GameBoard>> PlayTurn([FromBody] PlayTurnCommand command) =>
           HandleResult(await _mediator.Send(command));
    }
}