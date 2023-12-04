using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Domain.Enums;
using CloudTicTacToe.Domain.Models;

namespace CloudTicTacToe.Application.Helpers
{
    public static class GameBoardHelper
    {
        private static Random random = new(); 
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

        public static async Task ClosePlayerAciveGames(Player player, IGameBoardRepository gameBoardRepository)
        {
            var playerGames = await gameBoardRepository.GetAllAsync(g => g.PlayerO == player || g.PlayerX == player);

            foreach (var game in playerGames)
            {
                gameBoardRepository.Delete(game);
            }
        }

        public static Guid GetRandomStartingUser(GameBoard gameBoard)
        {
            return random.Next(2) == 0 ? gameBoard.PlayerX.Id : gameBoard.PlayerO!.Id;
        }
    }
}
