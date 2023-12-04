using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Application.Results;
using MediatR;

namespace CloudTicTacToe.Application.Queries.Players.GetAllPlayers
{
    public record GetAllPlayersQuery : IRequest<Result<IEnumerable<PlayerResult>>>;
}
