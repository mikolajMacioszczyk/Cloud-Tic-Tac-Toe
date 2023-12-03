using AutoMapper;
using CloudTicTacToe.Application.Commands.Games.Results;
using CloudTicTacToe.Application.Helpers;
using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Domain.Enums;
using CloudTicTacToe.Domain.Models;
using MediatR;

namespace CloudTicTacToe.Application.Commands.Games.CreateOrJoinGame
{
    public class CreateOrJoinGameCommandHandler : IRequestHandler<CreateOrJoinGameCommand, Result<GameBoardResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateOrJoinGameCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GameBoardResult>> Handle(CreateOrJoinGameCommand command, CancellationToken cancellationToken)
        {
            var player = await _unitOfWork.PlayerRepository.GetByIDAsync(command.PlayerId);

            if (player is null)
            {
                return new NotFound(command.PlayerId);
            }

            var game = await _unitOfWork.GameBoardRepository.GetFirstOrDefaultAsync(g => g.PlayerX == null, $"{nameof(GameBoard.PlayerX)},{nameof(GameBoard.PlayerO)}");
            // TODO: lock and check again

            if (game != null) 
            {
                if (game.PlayerX.Id == player.Id) 
                {
                    return new Failure("Cannot create game with the same player X and O");
                }

                game.PlayerO = player;
                game.State = GameGoardState.Ongoing;
                _unitOfWork.GameBoardRepository.Update(game);
            }
            else
            {
                game = new GameBoard
                {
                    PlayerX = player,
                    PlayerO = null,
                    Cells = await GameBoardHelper.GenerateCellsForBoard(GameBoard.BOARD_SIZE, _unitOfWork.CellRepository),
                    State = GameGoardState.Waiting
                };

                await _unitOfWork.GameBoardRepository.AddAsync(game);
            }
            
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<GameBoardResult>(game);
        }
    }
}
