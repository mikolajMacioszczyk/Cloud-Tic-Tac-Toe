using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Domain.Enums;
using CloudTicTacToe.Domain.Models;

namespace CloudTicTacToe.Application.Services
{
    public class PointsService : IPointsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PointsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AssignPointsFor(GameBoard gameBoard, Player playerX, Player playerO)
        {
            if (gameBoard.State == GameBoardState.Draw)
            {
                playerX.Points += 1;
                _unitOfWork.PlayerRepository.Update(playerX);
                playerO.Points += 1;
                _unitOfWork.PlayerRepository.Update(playerO);
            }
            else if (gameBoard.State == GameBoardState.WinnX)
            {
                playerX.Points += 2;
                _unitOfWork.PlayerRepository.Update(playerX);
                playerO.Points -= 1;
                _unitOfWork.PlayerRepository.Update(playerO);
            }
            else if (gameBoard.State == GameBoardState.WinnO)
            {
                playerX.Points -= 1;
                _unitOfWork.PlayerRepository.Update(playerX);
                playerO.Points += 2;
                _unitOfWork.PlayerRepository.Update(playerO);
            }
        }
    }
}
