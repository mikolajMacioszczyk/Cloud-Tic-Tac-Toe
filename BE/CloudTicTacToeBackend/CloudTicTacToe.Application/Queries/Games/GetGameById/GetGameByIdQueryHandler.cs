using AutoMapper;
using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Application.Results;
using CloudTicTacToe.Domain.Models;
using MediatR;

namespace CloudTicTacToe.Application.Queries.Games.GetGameById
{
    public class GetGameByIdQueryHandler : IRequestHandler<GetGameByIdQuery, Result<GameBoardResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetGameByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GameBoardResult>> Handle(GetGameByIdQuery query, CancellationToken cancellationToken)
        {
            var game = await _unitOfWork.GameBoardRepository.GetByIDAsync(query.Id, $"{nameof(GameBoard.Cells)},{nameof(GameBoard.PlayerX)},{nameof(GameBoard.PlayerO)}");
            return _mapper.Map<GameBoardResult>(game);
        }
    }
}
