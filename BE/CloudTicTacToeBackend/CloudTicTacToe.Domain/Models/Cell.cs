using CloudTicTacToe.Domain.Enums;

namespace CloudTicTacToe.Domain.Models
{
    public class Cell
    {
        public GameBoard GameBoard { get; set; } = null!;
        public FieldState FieldState { get; set; }
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
    }
}
