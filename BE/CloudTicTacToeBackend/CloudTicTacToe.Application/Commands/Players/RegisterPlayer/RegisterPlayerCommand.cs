using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Domain.Models;
using MediatR;

namespace CloudTicTacToe.Application.Commands.Players.RegisterPlayer
{
    // TODO: Mapping
    public record RegisterPlayerCommand : IRequest<Result<Player>>;
}
