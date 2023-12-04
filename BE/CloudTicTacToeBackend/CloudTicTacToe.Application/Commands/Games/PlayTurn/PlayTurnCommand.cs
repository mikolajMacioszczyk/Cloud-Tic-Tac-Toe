using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Application.Results;
using MediatR;

namespace CloudTicTacToe.Application.Commands.Games.PlayTurn
{
    public record PlayTurnCommand(Guid Id, Guid PlayerId, int RowNumber, int ColNumber) : IRequest<Result<GameBoardResult>>;
}
