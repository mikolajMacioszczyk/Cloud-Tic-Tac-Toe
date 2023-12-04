using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Application.Results;
using MediatR;

namespace CloudTicTacToe.Application.Commands.Players.RegisterPlayer
{
    public record RegisterPlayerCommand(string Name) : IRequest<Result<PlayerResult>>;
}
