using CloudTicTacToe.Domain.Models;
using MediatR;

namespace CloudTicTacToe.Application.Commands.Players.RegisterPlayer
{
    // TODO: Result
    // TODO: Mapping
    public record RegisterPlayerCommand : IRequest<Player>;
}
