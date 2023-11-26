namespace CloudTicTacToe.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        public ICellRepository CellRepository { get; }
        public IPlayerRepository PlayerRepository { get; }
        public IGameBoardRepository GameBoardRepository { get; }

        Task SaveChangesAsync();
    }
}
