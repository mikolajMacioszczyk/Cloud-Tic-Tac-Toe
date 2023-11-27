using CloudTicTacToe.Domain.Models;
using CloudTicTacToe.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace CloudTicTacToe.Infrastructure
{
    public class TicTacToeContext : DbContext
    {
        public DbSet<GameBoard> GameBoards { get; set; } = null!;
        public DbSet<Cell> Cells { get; set; } = null!;
        public DbSet<Player> Players { get; set; } = null!;
        public TicTacToeContext(DbContextOptions<TicTacToeContext> options) : base(options) 
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new PlayerConfiguration());
            modelBuilder.ApplyConfiguration(new GameBoardConfiguration());
            modelBuilder.ApplyConfiguration(new CellConfiguration());
        }
    }
}
