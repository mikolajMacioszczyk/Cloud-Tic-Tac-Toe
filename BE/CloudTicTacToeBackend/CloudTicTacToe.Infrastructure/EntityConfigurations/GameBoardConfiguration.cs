using CloudTicTacToe.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudTicTacToe.Infrastructure.EntityConfigurations
{
    public class GameBoardConfiguration : IEntityTypeConfiguration<GameBoard>
    {
        public void Configure(EntityTypeBuilder<GameBoard> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
