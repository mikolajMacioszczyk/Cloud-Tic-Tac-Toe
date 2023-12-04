using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Application.Results;
using MediatR;

namespace CloudTicTacToe.Application.Queries.Players.GetAllPlayers
{
    public record GetAllPlayersCommand : IRequest<Result<IEnumerable<PlayerResult>>>;
}
