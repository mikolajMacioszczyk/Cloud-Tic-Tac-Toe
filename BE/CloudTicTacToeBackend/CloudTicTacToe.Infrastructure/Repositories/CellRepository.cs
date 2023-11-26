using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Domain.Models;

namespace CloudTicTacToe.Infrastructure.Repositories
{
    public class CellRepository : RepositoryBase<Cell>, ICellRepository
    {
        public CellRepository(TicTacToeContext context) : base(context)
        {}
    }
}
