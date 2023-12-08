using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Application.Results;
using CloudTicTacToe.Domain.Models;
using CloudTicTacToe.Application.Extensions;
using MediatR;
using AutoMapper;
using CloudTicTacToe.Domain.Enums;
using CloudTicTacToe.Application.Services;

namespace CloudTicTacToe.Application.Commands.Games.Surrender
{
    public class SurrenderCommandHandler : IRequestHandler<SurrenderCommand, Result<GameBoardResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPointsService _pointsService;

        public SurrenderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IPointsService pointsService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _pointsService = pointsService;
        }

        public async Task<Result<GameBoardResult>> Handle(SurrenderCommand command, CancellationToken cancellationToken)
        {
            var game = await _unitOfWork.GameBoardRepository.GetByIDAsync(command.Id, $"{nameof(GameBoard.PlayerO)},{nameof(GameBoard.PlayerX)}");

            if (game is null)
            {
                return new NotFound(command.Id);
            }

            if (game.IsCompletedGame())
            {
                return new Failure($"Game already completed with state = {game.State}");
            }

            if (game.PlayerX.Id == command.PlayerId)
            {
                game.State = GameBoardState.WinnO;
            }
            else if (game.PlayerO?.Id == command.PlayerId)
            {
                game.State = GameBoardState.WinnX;
            }
            else
            {
                return new Failure($"Player with id {command.PlayerId} is not part of the game");
            }

            _pointsService.AssignPointsFor(game, game.PlayerX, game.PlayerO!);

            return _mapper.Map<GameBoardResult>(game);
        }
    }
}
