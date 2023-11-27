using CloudTicTacToe.Domain.Enums;
using CloudTicTacToe.Domain.Models;

namespace CloudTicTacToe.Application.Interfaces
{
    public interface IGameBoardStateService
    {
        GameGoardState CheckState(GameBoard gameBoard);
    }
}
