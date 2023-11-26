using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Domain.Models;

namespace CloudTicTacToe.Infrastructure.Repositories
{
    public class GameBoardRepository : RepositoryBase<GameBoard>, IGameBoardRepository
    {
        public GameBoardRepository(TicTacToeContext context) : base(context)
        {}
    }
}
