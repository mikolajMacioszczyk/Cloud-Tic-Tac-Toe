using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Domain.Enums;
using CloudTicTacToe.Domain.Models;
using CloudTicTacToe.Application.Extensions;
using MediatR;

namespace CloudTicTacToe.Application.Commands.Games.PlayTurn
{
    public class PlayTurnCommandHandler : IRequestHandler<PlayTurnCommand, Result<GameBoard>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IComputerPlayerService _computerPlayerService;

        public PlayTurnCommandHandler(IUnitOfWork unitOfWork, IComputerPlayerService computerPlayerService)
        {
            _unitOfWork = unitOfWork;
            _computerPlayerService = computerPlayerService;
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

            cell.FieldState = command.UserMark.ToFieldState();
            _unitOfWork.CellRepository.Update(cell);

            var winner = GetWinner(game.Cells);
            if (winner.HasValue)
            {
                game.Winner = winner;
            }
            else
            {
                _computerPlayerService.PlayComputerTurn(game.Cells, ToOppositeMark(command.UserMark));

                winner = GetWinner(game.Cells);
                if (winner.HasValue)
                {
                    game.Winner = winner;
                }
            }

            _unitOfWork.GameBoardRepository.Update(game);
            await _unitOfWork.SaveChangesAsync();

            return game;
        }

        private static UserMark ToOppositeMark(UserMark userMark) =>
            userMark switch
            {
                UserMark.X => UserMark.X,
                UserMark.O => UserMark.O,
                _ => throw new ArgumentOutOfRangeException(userMark.ToString())
            };

    private static UserMark? GetWinner(IEnumerable<Cell> cells)
        {
            for (var i = 0; i < GameBoard.BOARD_SIZE; i++)
            {
                var fromRow = GetWinnerFromLine(cells.Where(c => c.RowNumber == i));
                if (fromRow != null)
                {
                    return fromRow;
                }
                var fromColumn = GetWinnerFromLine(cells.Where(c => c.ColumnNumber == i));
                if (fromColumn != null)
                {
                    return fromColumn;
                }
            }

            return GetWinnerFromLine(cells.Where(c => c.ColumnNumber == c.RowNumber))
                ?? GetWinnerFromLine(cells.Where(c => c.ColumnNumber == GameBoard.BOARD_SIZE - c.RowNumber));
        }

        private static UserMark? GetWinnerFromLine(IEnumerable<Cell> line)
        {
            var fromLine = new HashSet<FieldState>(line
                .Select(c => c.FieldState))
                .Where(c => c != FieldState.Empty);

            if (fromLine.Count() == 1)
            {
                switch (fromLine.First())
                {
                    case FieldState.X:
                        return UserMark.X;
                    case FieldState.O:
                        return UserMark.O;
                }
            }

            return null;
        }
    }
}
