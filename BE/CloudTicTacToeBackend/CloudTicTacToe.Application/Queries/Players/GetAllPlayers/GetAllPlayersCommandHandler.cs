using AutoMapper;
using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Application.Results;
using MediatR;

namespace CloudTicTacToe.Application.Queries.Players.GetAllPlayers
{
    public class GetAllPlayersCommandHandler : IRequestHandler<GetAllPlayersCommand, Result<IEnumerable<PlayerResult>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllPlayersCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<PlayerResult>>> Handle(GetAllPlayersCommand command, CancellationToken cancellationToken)
        {
            var players = await _unitOfWork.PlayerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PlayerResult>>(players).ToList();
        }
    }
}
