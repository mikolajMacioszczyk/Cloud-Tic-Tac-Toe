using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Domain.Enums;
using CloudTicTacToe.Domain.Models;
using CloudTicTacToe.Application.Extensions;
using MediatR;

namespace CloudTicTacToe.Application.Commands.Games.PlayTurn
{
    public class PlayTurnCommandHandler : IRequestHandler<PlayTurnCommand, Result<GameBoard>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IComputerPlayerService _computerPlayerService;
        private readonly IGameBoardStateService _gameBoardStateService;

        public PlayTurnCommandHandler(IUnitOfWork unitOfWork, IComputerPlayerService computerPlayerService, IGameBoardStateService gameBoardStateService)
        {
            _unitOfWork = unitOfWork;
            _computerPlayerService = computerPlayerService;
            _gameBoardStateService = gameBoardStateService;
        }

        public async Task<Result<GameBoard>> Handle(PlayTurnCommand command, CancellationToken cancellationToken)
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

            var cell = game.Cells.FirstOrDefault(c => c.RowNumber == command.RowNumber && c.ColumnNumber == command.ColNumber);
            if (cell is null)
            {
                return new Failure($"Not found cell with row = {command.RowNumber} and column = {command.ColNumber}");
            }

            if (cell.FieldState != FieldState.Empty)
            {
                return new Failure($"Cell already merked with {cell.FieldState}");
            }

            cell.FieldState = command.UserMark.ToFieldState();
            _unitOfWork.CellRepository.Update(cell);

            game.State = _gameBoardStateService.CheckState(game);
            if (game.State == GameGoardState.Ongoing)
            {
                _computerPlayerService.PlayComputerTurn(game.Cells, ToOppositeMark(command.UserMark));

                game.State = _gameBoardStateService.CheckState(game);
            }

            _unitOfWork.GameBoardRepository.Update(game);
            await _unitOfWork.SaveChangesAsync();

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
