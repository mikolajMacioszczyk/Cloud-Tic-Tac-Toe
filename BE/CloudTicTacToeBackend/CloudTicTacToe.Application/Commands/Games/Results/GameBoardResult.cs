using CloudTicTacToe.Domain.Enums;

namespace CloudTicTacToe.Application.Commands.Games.Results
{
    public class GameBoardResult
    {
        public Guid Id { get; set; }
        public GameGoardState State { get; set; }
        public IEnumerable<IEnumerable<CellResult>> Board { get; set; } = null!;
    }
}
