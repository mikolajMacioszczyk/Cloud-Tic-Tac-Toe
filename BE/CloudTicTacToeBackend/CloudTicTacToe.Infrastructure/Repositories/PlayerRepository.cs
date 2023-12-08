using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudTicTacToe.Infrastructure.Repositories
{
    public class PlayerRepository : RepositoryBase<Player>, IPlayerRepository
    {
        public PlayerRepository(TicTacToeContext context) : base(context)
        {}

        public Task<Player> GetComputerPlayer()
            => context.Players.FirstAsync(p => p.IsComputer);
    }
}
