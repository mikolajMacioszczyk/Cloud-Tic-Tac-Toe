using AutoMapper;
using CloudTicTacToe.Application.Commands.Games.Results;
using CloudTicTacToe.Domain.Models;

namespace CloudTicTacToe.Application.Profiles.Resolvers
{
    public class BoardResolver : IValueResolver<GameBoard, GameBoardResult, IEnumerable<IEnumerable<CellResult>>>
    {
        public IEnumerable<IEnumerable<CellResult>> Resolve(GameBoard source, GameBoardResult destination, IEnumerable<IEnumerable<CellResult>> destMember, ResolutionContext context)
        {
            return source.Cells
                .GroupBy(s => s.RowNumber)
                .OrderBy(g => g.Key)
                .Select(g => g.Select(cell => context.Mapper.Map<CellResult>(cell)).OrderBy(c => c.ColumnNumber).ToList())
                .ToList();
        }
    }
}
