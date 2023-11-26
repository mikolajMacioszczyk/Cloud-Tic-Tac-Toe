using CloudTicTacToe.Application.Commands.Games.InitializeGameWithComputer;
using CloudTicTacToe.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CloudTicTacToe.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GameController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost()]
        public async Task<ActionResult<GameBoard>> InitializeGameWithComputer([FromBody] InitializeGameWithComputerCommand command) =>
            Ok(await _mediator.Send(command));
    }
}