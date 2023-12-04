using CloudTicTacToe.Domain.Enums;

namespace CloudTicTacToe.Application.Results
{
    public class GameBoardResult
    {
        public Guid Id { get; set; }
        public GameGoardState State { get; set; }
        public IEnumerable<IEnumerable<CellResult>> Board { get; set; } = null!;
        public Guid NextPlayerId { get; set; }
        public PlayerResult PlayerX { get; set; } = null!;
        public PlayerResult? PlayerO { get; set; }
    }
}
