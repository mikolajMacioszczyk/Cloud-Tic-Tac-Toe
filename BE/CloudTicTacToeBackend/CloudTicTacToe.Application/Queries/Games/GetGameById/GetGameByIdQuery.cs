using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Application.Results;
using MediatR;

namespace CloudTicTacToe.Application.Queries.Games.GetGameById
{
    public record GetGameByIdQuery(Guid Id) : IRequest<Result<GameBoardResult>>;
}
