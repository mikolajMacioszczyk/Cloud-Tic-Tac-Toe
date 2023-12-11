using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Application.Queries.Games.GetGameById;
using CloudTicTacToe.Application.Services;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace CloudTicTacToe.API.Hubs
{
    public class GameHub : Hub
    {
        private const string GroupName = "Game";
        private const string UserConnectedResponse = "UserConnected";
        private const string BoardUpdatedResponse = "BoardUpdated";
        private readonly IGameConnectionService _connectionService;
        private readonly IMediator _mediator;

        public GameHub(IGameConnectionService connectionService, IMediator mediator)
        {
            _connectionService = connectionService;
            _mediator = mediator;
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

        public async Task NotifyTurnPlayed(Guid gameId)
        {
            await DisplayBoard(gameId);
        }

        public async Task DisplayBoard(Guid gameId)
        {
            var gameBoardResult = await _mediator.Send(new GetGameByIdQuery(gameId));
            if (gameBoardResult.IsSuccess)
            {
                await Clients.Groups(GroupName).SendAsync(BoardUpdatedResponse, gameBoardResult.Value);
            }
        }
    }
}
