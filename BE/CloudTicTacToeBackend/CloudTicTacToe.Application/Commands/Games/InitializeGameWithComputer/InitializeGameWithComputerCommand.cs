using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Domain.Models;
using MediatR;

namespace CloudTicTacToe.Application.Commands.Games.InitializeGameWithComputer
{
    public record InitializeGameWithComputerCommand(Guid PlayerId) : IRequest<Result<GameBoard>>;
}
