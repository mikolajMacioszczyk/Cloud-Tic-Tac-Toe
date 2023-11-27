using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Domain.Models;
using MediatR;

namespace CloudTicTacToe.Application.Commands.Players.RegisterPlayer
{
    public class RegisterPlayerCommandHandler : IRequestHandler<RegisterPlayerCommand, Result<Player>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterPlayerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Player>> Handle(RegisterPlayerCommand command, CancellationToken cancellationToken)
        {
            var player = new Player() { IsComputer = false };

            await _unitOfWork.PlayerRepository.AddAsync(player);
            await _unitOfWork.SaveChangesAsync();

            return player;
        }
    }
}
