using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Application.Results;
using MediatR;

namespace CloudTicTacToe.Application.Commands.Games.InitializeGameWithComputer
{
    public record InitializeGameWithComputerCommand(Guid PlayerId) : IRequest<Result<GameBoardResult>>;
}
