using CloudTicTacToe.Domain.Models;
using MediatR;

namespace CloudTicTacToe.Application.Commands.Games.InitializeGameWithComputer
{
    // TODO: Result
    // TODO: Mapping
    public record InitializeGameWithComputerCommand(Guid PlayerId) : IRequest<GameBoard>;
}
