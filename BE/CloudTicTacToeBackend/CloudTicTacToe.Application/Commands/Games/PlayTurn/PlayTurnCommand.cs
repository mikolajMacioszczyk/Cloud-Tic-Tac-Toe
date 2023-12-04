﻿using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Application.Results;
using CloudTicTacToe.Domain.Enums;
using MediatR;

namespace CloudTicTacToe.Application.Commands.Games.PlayTurn
{
    public record PlayTurnCommand(Guid Id, UserMark UserMark, int RowNumber, int ColNumber) : IRequest<Result<GameBoardResult>>;
}
