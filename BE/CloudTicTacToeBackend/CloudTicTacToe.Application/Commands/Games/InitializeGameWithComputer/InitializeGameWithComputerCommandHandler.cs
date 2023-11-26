using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Domain.Enums;
using CloudTicTacToe.Domain.Models;
using MediatR;

namespace CloudTicTacToe.Application.Commands.Games.InitializeGameWithComputer
{
    public class InitializeGameWithComputerCommandHandler : IRequestHandler<InitializeGameWithComputerCommand, GameBoard>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InitializeGameWithComputerCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GameBoard> Handle(InitializeGameWithComputerCommand command, CancellationToken cancellationToken)
        {
            // TODO: Handle not found
            Player player = (await _unitOfWork.PlayerRepository.GetByIDAsync(command.PlayerId))!;

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
