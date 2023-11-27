﻿using CloudTicTacToe.Application.Interfaces;
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

            // TODO: check draw

            return GameGoardState.Ongoing;
        }

        private static FieldState? GetWinnerFromLine(IEnumerable<Cell> line)
        {
            var fromLine = new HashSet<FieldState>(line
                .Select(c => c.FieldState))
                .Where(c => c != FieldState.Empty);

            if (fromLine.Count() == 1)
            {
                switch (fromLine.First())
                {
                    case FieldState.X:
                    case FieldState.O:
                        return fromLine.First();
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