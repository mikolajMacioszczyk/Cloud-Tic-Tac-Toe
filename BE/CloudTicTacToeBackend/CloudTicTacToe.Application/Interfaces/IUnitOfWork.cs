using CloudTicTacToe.Domain.Models;

namespace CloudTicTacToe.Application.Interfaces
{
    public interface IUnitOfWork
    {
        public ICellRepository CellRepository { get; }
        public IPlayerRepository PlayerRepository { get; }
        public IGameBoardRepository GameBoardRepository { get; }

        Task<(bool ChangesMade, IEnumerable<BaseDomainModel> EntitiesWithErrors)> SaveChangesAsync();
    }
}
