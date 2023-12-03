using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Domain.Enums;
using CloudTicTacToe.Domain.Models;

namespace CloudTicTacToe.Application.Helpers
{
    public static class GameBoardHelper
    {
        public static async Task<IEnumerable<Cell>> GenerateCellsForBoard(int boardSize, ICellRepository cellRepository)
        {
            var collection = new Cell[boardSize * boardSize];
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    collection[i * boardSize + j] = new Cell()
                    {
                        RowNumber = i,
                        ColumnNumber = j,
                        FieldState = FieldState.Empty
                    };
                    await cellRepository.AddAsync(collection[i * boardSize + j]);
                }
            }

            return collection;
        }
    }
}
