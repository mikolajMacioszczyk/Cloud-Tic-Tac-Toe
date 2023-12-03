using CloudTicTacToe.Application.Commands.Games.Results;
using CloudTicTacToe.Application.Models;
using MediatR;

namespace CloudTicTacToe.Application.Commands.Games.CreateOrJoinGame
{
    public record CreateOrJoinGameCommand(Guid PlayerId) : IRequest<Result<GameBoardResult>>;
}
