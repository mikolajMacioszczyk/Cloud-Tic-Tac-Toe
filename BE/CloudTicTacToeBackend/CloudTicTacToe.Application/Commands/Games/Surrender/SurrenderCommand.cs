using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Application.Results;
using MediatR;

namespace CloudTicTacToe.Application.Commands.Games.Surrender
{
    public record SurrenderCommand(Guid Id, Guid PlayerId) : IRequest<Result<GameBoardResult>>;
}
