using AutoMapper;
using CloudTicTacToe.Application.Helpers;
using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Application.Results;
using CloudTicTacToe.Domain.Enums;
using CloudTicTacToe.Domain.Models;
using MediatR;

namespace CloudTicTacToe.Application.Commands.Games.CreateOrJoinGame
{
    public class CreateOrJoinGameCommandHandler : IRequestHandler<CreateOrJoinGameCommand, Result<GameBoardResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly object mutex = new();

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

            await GameBoardHelper.ClosePlayerAciveGames(player, _unitOfWork.GameBoardRepository);

            var (addedToExisting, game) = await TryUpdateExistingGame(player);

            if (!addedToExisting)
            {
                game = new GameBoard
                {
                    PlayerX = player,
                    PlayerO = null,
                    Cells = await GameBoardHelper.GenerateCellsForBoard(GameBoard.BOARD_SIZE, _unitOfWork.CellRepository),
                    State = GameGoardState.Waiting
                };

                await _unitOfWork.GameBoardRepository.AddAsync(game);
                await _unitOfWork.SaveChangesAsync();
            }

            return _mapper.Map<GameBoardResult>(game);
        }

        private async Task<(bool, GameBoard?)> TryUpdateExistingGame(Player player)
        {
            var game = await _unitOfWork.GameBoardRepository.GetFirstOrDefaultAsync(g => g.PlayerO == null, $"{nameof(GameBoard.PlayerX)},{nameof(GameBoard.PlayerO)},{nameof(GameBoard.Cells)}");

            if (game != null)
            {
                if (game.PlayerX.Id == player.Id)
                {
                    return (false, null);
                }

                game.PlayerO = player;
                game.State = GameGoardState.Ongoing;
                game.NextPlayerId = GameBoardHelper.GetRandomStartingUser(game);

                lock (mutex)
                {
                    var existing = _unitOfWork.GameBoardRepository.GetFirstOrDefault(g => g.PlayerO == null);

                    if (game.Id != existing?.Id) 
                    {
                        return (false, null);
                    }

                    _unitOfWork.GameBoardRepository.Update(game);
                    _unitOfWork.SaveChangesAsync().Wait();
                }

                return (true, game);
            }
            return (false, null);
        }
    }
}
