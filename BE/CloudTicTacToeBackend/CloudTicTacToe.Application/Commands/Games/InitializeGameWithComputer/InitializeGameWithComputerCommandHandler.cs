using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Domain.Enums;
using CloudTicTacToe.Domain.Models;
using MediatR;

namespace CloudTicTacToe.Application.Commands.Games.InitializeGameWithComputer
{
    public class InitializeGameWithComputerCommandHandler : IRequestHandler<InitializeGameWithComputerCommand, Result<GameBoard>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InitializeGameWithComputerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GameBoard>> Handle(InitializeGameWithComputerCommand command, CancellationToken cancellationToken)
        {
            var player = (await _unitOfWork.PlayerRepository.GetByIDAsync(command.PlayerId));

            if (player is null)
            {
                return new NotFound(command.PlayerId);
            }

            var game = new GameBoard
            {
                PlayerX = player,
                PlayerO = new Player() { IsComputer = true },
                Cells = await GenerateCellsForBoard(GameBoard.BOARD_SIZE),
            };

            await _unitOfWork.GameBoardRepository.AddAsync(game);
            await _unitOfWork.SaveChangesAsync();

            return game;
        }

        private async Task<IEnumerable<Cell>> GenerateCellsForBoard(int boardSize)
        {
            var collection = new Cell[boardSize * boardSize];
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    collection[i * boardSize + j] = new Cell() 
                    { 
                        RowNumber = i, 
                        ColumnNumber = j, 
                        FieldState = FieldState.Empty
                    };
                    await _unitOfWork.CellRepository.AddAsync(collection[i * boardSize + j]);
                }
            }

            return collection;
        }
    }
}
