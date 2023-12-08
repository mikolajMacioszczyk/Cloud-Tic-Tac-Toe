using CloudTicTacToe.Domain.Models;

namespace CloudTicTacToe.Application.Interfaces
{
    public interface IPlayerRepository : IBaseEntityRepository<Player>
    {
        Task<Player> GetComputerPlayer();
    }
}
