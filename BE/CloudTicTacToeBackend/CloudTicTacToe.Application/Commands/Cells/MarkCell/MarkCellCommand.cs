namespace CloudTicTacToe.Application.Commands.Cells.MarkCell
{
    public record MarkCellCommand(Guid GameId, Guid PlayerId, int RowNumber, int ColNumber);
}
