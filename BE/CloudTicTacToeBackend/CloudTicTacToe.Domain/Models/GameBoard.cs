namespace CloudTicTacToe.Domain.Models
{
    public class GameBoard
    {
        public const int BOARD_SIZE = 3;
        public Player Player1 { get; set; } = null!;
        public Player Player2 { get; set; } = null!;
        public IEnumerable<Cell> Cells { get; set; } = null!;
    }
}
