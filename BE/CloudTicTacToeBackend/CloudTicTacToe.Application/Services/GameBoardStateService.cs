using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Domain.Enums;
using CloudTicTacToe.Domain.Models;

namespace CloudTicTacToe.Application.Services
{
    public class GameBoardStateService : IGameBoardStateService
    {
        public GameGoardState CheckState(GameBoard gameBoard)
        {
            var cells = gameBoard.Cells;
            for (var i = 0; i < GameBoard.BOARD_SIZE; i++)
            {
                var fromRow = GetWinnerFromLine(cells.Where(c => c.RowNumber == i));
                if (fromRow.HasValue)
                {
                    return ConvertFromFieldStateToGameBoardState(fromRow.Value);
                }
                var fromColumn = GetWinnerFromLine(cells.Where(c => c.ColumnNumber == i));
                if (fromColumn.HasValue)
                {
                    return ConvertFromFieldStateToGameBoardState(fromColumn.Value);
                }
            }

            var fromLeftDiagonal = GetWinnerFromLine(cells.Where(c => c.ColumnNumber == c.RowNumber));
            if (fromLeftDiagonal.HasValue)
            {
                return ConvertFromFieldStateToGameBoardState(fromLeftDiagonal.Value);
            }

            var fromRightDiagonal = GetWinnerFromLine(cells.Where(c => c.ColumnNumber == GameBoard.BOARD_SIZE - c.RowNumber));
            if (fromLeftDiagonal.HasValue)
            {
                return ConvertFromFieldStateToGameBoardState(fromLeftDiagonal.Value);
            }

            if (cells.All(c => c.FieldState != FieldState.Empty))
            {
                return GameGoardState.Draw;
            }

            return GameGoardState.Ongoing;
        }

        private static FieldState? GetWinnerFromLine(IEnumerable<Cell> line)
        {
            var fromLine = line
                .Select(c => c.FieldState)
                .Where(c => c != FieldState.Empty);

            bool isWin = fromLine.Count() == GameBoard.BOARD_SIZE && fromLine.Distinct().Count() == 1;
            if (isWin)
            {
                switch (fromLine.First())
                {
                    case FieldState.X:
                    case FieldState.O:
                        return fromLine.First();
                    default:
                        throw new ArgumentOutOfRangeException(fromLine.ToString());
                }
            }

            return null;
        }

        private static GameGoardState ConvertFromFieldStateToGameBoardState(FieldState fieldState)
        {
            switch (fieldState)
            {
                case FieldState.X:
                    return GameGoardState.WinnX;
                case FieldState.O:
                    return GameGoardState.WinnO;
                default:
                    throw new ArgumentOutOfRangeException(fieldState.ToString());
            }
        }
    }
}
