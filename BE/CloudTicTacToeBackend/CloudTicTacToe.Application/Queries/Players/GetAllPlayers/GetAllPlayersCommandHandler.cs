using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Domain.Models;
using MediatR;

namespace CloudTicTacToe.Application.Queries.Players.GetAllPlayers
{
    public class GetAllPlayersCommandHandler : IRequestHandler<GetAllPlayersCommand, Result<IEnumerable<Player>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllPlayersCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<Player>>> Handle(GetAllPlayersCommand command, CancellationToken cancellationToken)
        {
            var players = await _unitOfWork.PlayerRepository.GetAllAsync();
            return players.ToList();
        }
    }
}
