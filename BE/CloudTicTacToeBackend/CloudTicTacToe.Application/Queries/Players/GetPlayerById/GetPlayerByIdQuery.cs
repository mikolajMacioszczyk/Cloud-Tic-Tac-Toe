using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Application.Results;
using MediatR;

namespace CloudTicTacToe.Application.Queries.Players.GetPlayerById
{
    public record GetPlayerByIdQuery(Guid Id) : IRequest<Result<PlayerResult>>;
}
