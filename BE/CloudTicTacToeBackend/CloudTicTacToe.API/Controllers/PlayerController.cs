﻿using CloudTicTacToe.API.BaseClasses;
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

        [HttpPost()]
        public async Task<ActionResult<Player>> RegisterPlayer([FromBody] RegisterPlayerCommand command) =>
            Ok(await _mediator.Send(command));
    }
}
