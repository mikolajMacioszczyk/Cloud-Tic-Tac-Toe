using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Domain.Models;
using MediatR;

namespace CloudTicTacToe.Application.Commands.Players.RegisterPlayer
{
    public class RegisterPlayerCommandHandler : IRequestHandler<RegisterPlayerCommand, Player>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterPlayerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Player> Handle(RegisterPlayerCommand command, CancellationToken cancellationToken)
        {
            var player = new Player() { IsComputer = true };

            await _unitOfWork.PlayerRepository.AddAsync(player);
            await _unitOfWork.SaveChangesAsync();

            return player;
        }
    }
}
