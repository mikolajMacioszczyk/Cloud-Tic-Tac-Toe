using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Domain.Models;

namespace CloudTicTacToe.Application.Interfaces
{
    public interface IPointsService
    {
        void AssignPointsFor(GameBoard gameBoard, Player playerX, Player playerO);
    }
}
