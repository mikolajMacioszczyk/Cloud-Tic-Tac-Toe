using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Application.Results;
using MediatR;

namespace CloudTicTacToe.Application.Commands.Players.RegisterOrLoginPlayer
{
    public record RegisterOrLoginPlayerCommand(string Name) : IRequest<Result<PlayerResult>>;
}
