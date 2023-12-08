using CloudTicTacToe.Domain.Enums;
using CloudTicTacToe.Domain.Models;

namespace CloudTicTacToe.Application.Extensions
{
    public static class GameBoardExtensions
    {
        public static bool IsCompletedGame(this GameBoard gameBoard) =>
            gameBoard.State switch
            {
                GameBoardState.Draw or GameBoardState.WinnO or GameBoardState.WinnX => true,
                _ => false
            };
    }
}
