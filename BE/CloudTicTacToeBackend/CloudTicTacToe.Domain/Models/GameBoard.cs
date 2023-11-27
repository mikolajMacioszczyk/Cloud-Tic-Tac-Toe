using CloudTicTacToe.Domain.Enums;

namespace CloudTicTacToe.Domain.Models
{
    public class GameBoard : BaseDomainModel
    {
        public const int BOARD_SIZE = 3;
        public Player PlayerX { get; set; } = null!;
        public Player PlayerO { get; set; } = null!;
        public GameGoardState State { get; set; }
        public IEnumerable<Cell> Cells { get; set; } = null!;
    }
}
