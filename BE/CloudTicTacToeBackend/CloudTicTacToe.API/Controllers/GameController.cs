using CloudTicTacToe.API.BaseClasses;
using CloudTicTacToe.Application.Commands.Games.CreateOrJoinGame;
using CloudTicTacToe.Application.Commands.Games.InitializeGameWithComputer;
using CloudTicTacToe.Application.Commands.Games.PlayTurn;
using CloudTicTacToe.Application.Commands.Games.Surrender;
using CloudTicTacToe.Application.Queries.Games.GetGameById;
using CloudTicTacToe.Application.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CloudTicTacToe.API.Controllers
{
    public class GameController : BaseApiController
    {
        public GameController(IMediator mediator) : base(mediator)
        {}

        [HttpGet("{id}")]
        public async Task<ActionResult<GameBoardResult>> GetGameById([FromRoute] GetGameByIdQuery query) =>
            HandleResult(await _mediator.Send(query));

        [HttpPost()]
        public async Task<ActionResult<GameBoardResult>> InitializeGameWithComputer([FromBody] InitializeGameWithComputerCommand command) =>
            HandleResult(await _mediator.Send(command));

        [HttpPost("online")]
        public async Task<ActionResult<GameBoardResult>> CreateOrJoinGame([FromBody] CreateOrJoinGameCommand command) =>
            HandleResult(await _mediator.Send(command));

        [HttpPut("{Id}/actions/play")]
        public async Task<ActionResult<GameBoardResult>> PlayTurn([FromBody] PlayTurnCommand command) =>
           HandleResult(await _mediator.Send(command));

        [HttpPut("{Id}/actions/surrender")]
        public async Task<ActionResult<GameBoardResult>> Surrender([FromBody] SurrenderCommand command) =>
           HandleResult(await _mediator.Send(command));
    }
}