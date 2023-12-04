using CloudTicTacToe.Domain.Enums;

namespace CloudTicTacToe.Application.Results
{
    public class CellResult
    {
        public Guid Id { get; set; }
        public FieldState FieldState { get; set; }
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
    }
}
