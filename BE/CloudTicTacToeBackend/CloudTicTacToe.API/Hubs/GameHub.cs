using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Application.Services;
using Microsoft.AspNetCore.SignalR;

namespace CloudTicTacToe.API.Hubs
{
    public class GameHub : Hub
    {
        private const string GroupName = "Game";
        private const string UserConnectedResponse = "UserConnected";
        private const string BoardUpdatedResponse = "BoardUpdated";
        private readonly GameConnectionService _connectionService;
        private readonly IUnitOfWork _unitOfWork;

        public GameHub(GameConnectionService connectionService, IUnitOfWork unitOfWork)
        {
            _connectionService = connectionService;
            _unitOfWork = unitOfWork;
        }

        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, GroupName);
            await Clients.Caller.SendAsync(UserConnectedResponse);
        }

        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, GroupName);

            var gameId = _connectionService.GetGameByConnnectionId(Context.ConnectionId);
            if (gameId != null)
            {
                _connectionService.RemoveUserFromGame(gameId.Value, Context.ConnectionId);
                await DisplayBoard(gameId.Value);
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task AddGameConnectionId(Guid gameId)
        {
            _connectionService.AddGameConnnectionId(gameId, Context.ConnectionId);
            await DisplayBoard(gameId);
        }

        // TODO: Doubled requests
        public async Task DisplayBoard(Guid gameId)
        {
            var gameBoard = await  _unitOfWork.GameBoardRepository.GetByIDAsync(gameId);
            if (gameBoard != null)
            {
                await Clients.Groups(GroupName).SendAsync(BoardUpdatedResponse, gameBoard);
            }
        }
    }
}
