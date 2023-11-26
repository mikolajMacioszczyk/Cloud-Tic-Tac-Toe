using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Domain.Enums;
using CloudTicTacToe.Domain.Models;
using MediatR;

namespace CloudTicTacToe.Application.Commands.Games.PlayTurn
{
    public class PlayTurnCommandHandler : IRequestHandler<PlayTurnCommand, Result<GameBoard>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlayTurnCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GameBoard>> Handle(PlayTurnCommand command, CancellationToken cancellationToken)
        {
            var game = await _unitOfWork.GameBoardRepository.GetByIDAsync(command.Id, $"{nameof(GameBoard.PlayerO)},{nameof(GameBoard.PlayerX)},{nameof(GameBoard.Cells)}");

            if (game is null) 
            {
                return new NotFound(command.Id);
            }

            var cell = game.Cells.FirstOrDefault(c => c.RowNumber == command.RowNumber && c.ColumnNumber == command.ColNumber);
            if (cell is null)
            {
                return new Failure($"Not found cell with row = {command.RowNumber} and column = {command.ColNumber}");
            }

            if (cell.FieldState != FieldState.Empty)
            {
                return new Failure($"Cell already merked with {cell.FieldState}");
            }

            // assign cell

            // check if win

            // computer turn

            // check if win

            // update and save changes

            return game;
        }
    }
}
