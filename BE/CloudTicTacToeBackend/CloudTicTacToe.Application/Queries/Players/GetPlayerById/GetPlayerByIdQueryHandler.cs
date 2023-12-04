using AutoMapper;
using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Application.Results;
using MediatR;

namespace CloudTicTacToe.Application.Queries.Players.GetPlayerById
{
    public class GetPlayerByIdQueryHandler : IRequestHandler<GetPlayerByIdQuery, Result<PlayerResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPlayerByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PlayerResult>> Handle(GetPlayerByIdQuery query, CancellationToken cancellationToken)
        {
            var game = await _unitOfWork.PlayerRepository.GetByIDAsync(query.Id);
            if (game is null)
            {
                return new NotFound(query.Id);
            }

            return _mapper.Map<PlayerResult>(game);
        }
    }
}
