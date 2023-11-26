using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Domain.Models;
using MediatR;

namespace CloudTicTacToe.Application.Commands.Games.InitializeGameWithComputer
{
    // TODO: Mapping
    public record InitializeGameWithComputerCommand(Guid PlayerId) : IRequest<Result<GameBoard>>;
}
