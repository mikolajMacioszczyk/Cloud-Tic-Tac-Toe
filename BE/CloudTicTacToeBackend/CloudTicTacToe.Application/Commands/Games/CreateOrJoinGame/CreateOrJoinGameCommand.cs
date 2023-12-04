using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Application.Results;
using MediatR;

namespace CloudTicTacToe.Application.Commands.Games.CreateOrJoinGame
{
    public record CreateOrJoinGameCommand(Guid PlayerId) : IRequest<Result<GameBoardResult>>;
}
