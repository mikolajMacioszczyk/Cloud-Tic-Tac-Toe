using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Domain.Models;
using MediatR;

namespace CloudTicTacToe.Application.Commands.Players.RegisterPlayer
{
    public record RegisterPlayerCommand(string Name) : IRequest<Result<Player>>;
}
