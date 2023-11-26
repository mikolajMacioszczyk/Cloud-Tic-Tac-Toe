namespace CloudTicTacToe.Domain.Models
{
    public class GameBoard : BaseDomainModel
    {
        public const int BOARD_SIZE = 3;
        public Player PlayerX { get; set; } = null!;
        public Player PlayerO { get; set; } = null!;
        public Player? Winner { get; set; } = null!;
        public IEnumerable<Cell> Cells { get; set; } = null!;
    }
}
