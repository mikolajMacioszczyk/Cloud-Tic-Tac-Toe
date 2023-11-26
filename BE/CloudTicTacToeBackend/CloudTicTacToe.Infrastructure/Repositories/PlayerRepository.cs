using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Domain.Models;

namespace CloudTicTacToe.Infrastructure.Repositories
{
    public class PlayerRepository : RepositoryBase<Player>, IPlayerRepository
    {
        public PlayerRepository(TicTacToeContext context) : base(context)
        {}
    }
}
