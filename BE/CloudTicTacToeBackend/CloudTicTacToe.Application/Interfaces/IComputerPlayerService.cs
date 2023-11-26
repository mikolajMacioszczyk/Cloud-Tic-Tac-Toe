using CloudTicTacToe.Domain.Enums;
using CloudTicTacToe.Domain.Models;

namespace CloudTicTacToe.Application.Interfaces
{
    public interface IComputerPlayerService
    {
        void PlayComputerTurn(IEnumerable<Cell> cells, UserMark computerMark);
    }
}
