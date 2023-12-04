using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Domain.Enums;
using CloudTicTacToe.Domain.Models;
using CloudTicTacToe.Application.Extensions;
using MediatR;
using AutoMapper;
using CloudTicTacToe.Application.Results;

namespace CloudTicTacToe.Application.Commands.Games.PlayTurn
{
    public class PlayTurnCommandHandler : IRequestHandler<PlayTurnCommand, Result<GameBoardResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IComputerPlayerService _computerPlayerService;
        private readonly IGameBoardStateService _gameBoardStateService;
        private readonly IMapper _mapper;

        public PlayTurnCommandHandler(IUnitOfWork unitOfWork, IComputerPlayerService computerPlayerService, IGameBoardStateService gameBoardStateService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _computerPlayerService = computerPlayerService;
            _gameBoardStateService = gameBoardStateService;
            _mapper = mapper;
        }

        public async Task<Result<GameBoardResult>> Handle(PlayTurnCommand command, CancellationToken cancellationToken)
        {
            var game = await _unitOfWork.GameBoardRepository.GetByIDAsync(command.Id, $"{nameof(GameBoard.PlayerO)},{nameof(GameBoard.PlayerX)},{nameof(GameBoard.Cells)}");

            if (game is null) 
            {
                return new NotFound(command.Id);
            }

            if (game.State != GameGoardState.Ongoing)
            {
                return new Failure($"Game already completed with state = {game.State}");
            }

            bool isComputerGame = game.PlayerX.IsComputer || game.PlayerO!.IsComputer;

            var gameResult = PlayUserTurn(command, game);
            if (!gameResult.IsSuccess)
            {
                return gameResult.MapNonSuccessfullTo<GameBoardResult>();
            }
            if (isComputerGame)
            {
                gameResult = PlayComputerTurn(command, game);
                if (!gameResult.IsSuccess)
                {
                    return gameResult.MapNonSuccessfullTo<GameBoardResult>();
                }
            }
            _unitOfWork.GameBoardRepository.Update(game);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<GameBoardResult>(game);
        }

        private Result<GameBoard> PlayComputerTurn(PlayTurnCommand command, GameBoard game)
        {
            UserMark? userMark = command.PlayerId == game.PlayerX.Id ? UserMark.X : command.PlayerId == game.PlayerO!.Id ? UserMark.O : null;

            if (!userMark.HasValue)
            {
                return new Failure($"Player with id {command.PlayerId} is not part of the game");
            }

            if (game.State == GameGoardState.Ongoing)
            {
                _computerPlayerService.PlayComputerTurn(game.Cells, ToOppositeMark(userMark.Value));

                game.State = _gameBoardStateService.CheckState(game);
            }

            return game;
        }

        private Result<GameBoard> PlayUserTurn(PlayTurnCommand command, GameBoard game)
        {
            UserMark? userMark = command.PlayerId == game.PlayerX.Id ? UserMark.X : command.PlayerId == game.PlayerO!.Id ? UserMark.O : null;

            if (!userMark.HasValue)
            {
                return new Failure($"Player with id {command.PlayerId} is not part of the game");
            }

            var cell = game.Cells.FirstOrDefault(c => c.RowNumber == command.RowNumber && c.ColumnNumber == command.ColNumber);
            if (cell is null)
            {
                return new Failure($"Not found cell with row = {command.RowNumber} and column = {command.ColNumber}");
            }

            if (cell.FieldState != FieldState.Empty)
            {
                return new Failure($"Cell already merked with {cell.FieldState}");
            }

            cell.FieldState = userMark.Value.ToFieldState();
            _unitOfWork.CellRepository.Update(cell);

            game.State = _gameBoardStateService.CheckState(game);

            return game;
        }

        private static UserMark ToOppositeMark(UserMark userMark) =>
            userMark switch
            {
                UserMark.X => UserMark.O,
                UserMark.O => UserMark.X,
                _ => throw new ArgumentOutOfRangeException(userMark.ToString())
            };
    }
}
