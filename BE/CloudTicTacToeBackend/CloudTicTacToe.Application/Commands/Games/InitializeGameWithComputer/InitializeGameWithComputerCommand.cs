using CloudTicTacToe.Application.Commands.Games.Results;
using CloudTicTacToe.Application.Models;
using MediatR;

namespace CloudTicTacToe.Application.Commands.Games.InitializeGameWithComputer
{
    public record InitializeGameWithComputerCommand(Guid PlayerId) : IRequest<Result<GameBoardResult>>;
}
