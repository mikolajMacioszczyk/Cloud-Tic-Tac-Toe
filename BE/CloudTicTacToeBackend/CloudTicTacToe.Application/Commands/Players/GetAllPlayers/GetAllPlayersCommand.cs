using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Domain.Models;
using MediatR;

namespace CloudTicTacToe.Application.Commands.Players.GetAllPlayers
{
    public record GetAllPlayersCommand : IRequest<Result<IEnumerable<Player>>>;
}
