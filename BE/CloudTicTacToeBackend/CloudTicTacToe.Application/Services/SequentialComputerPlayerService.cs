using CloudTicTacToe.Application.Extensions;
using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Domain.Enums;
using CloudTicTacToe.Domain.Models;

namespace CloudTicTacToe.Application.Services
{
    public class SequentialComputerPlayerService : IComputerPlayerService
    {
        public void PlayComputerTurn(IEnumerable<Cell> cells, UserMark computerMark)
        {
            foreach (var cell in cells)
            {
                if (cell.FieldState == FieldState.Empty)
                {
                    cell.FieldState = computerMark.ToFieldState();
                    return;
                }
            }

            throw new Exception("No empty field to make mark");
        }
    }
}
