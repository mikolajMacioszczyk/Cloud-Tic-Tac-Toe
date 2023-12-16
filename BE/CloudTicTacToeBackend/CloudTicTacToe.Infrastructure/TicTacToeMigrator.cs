using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CloudTicTacToe.Infrastructure
{
    public class TicTacToeMigrator : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly bool _migrate;

        public TicTacToeMigrator(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _migrate = !configuration.GetSection("UseInMemoryDb").Value.Equals("true", StringComparison.OrdinalIgnoreCase);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var ctx = scope.ServiceProvider.GetRequiredService<TicTacToeContext>();
            if (_migrate)
            {
                ctx.Database.Migrate();
            }

            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            await InitializeComputerPlayer(unitOfWork);
        }

        private static async Task InitializeComputerPlayer(IUnitOfWork unitOfWork)
        {
            var existing = await unitOfWork.PlayerRepository.GetFirstOrDefaultAsync(p => p.IsComputer);
            if (existing is null)
            {
                await unitOfWork.PlayerRepository.AddAsync(new Player
                {
                    IsComputer = true,
                    Name = "Computer"
                });
                await unitOfWork.SaveChangesAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
