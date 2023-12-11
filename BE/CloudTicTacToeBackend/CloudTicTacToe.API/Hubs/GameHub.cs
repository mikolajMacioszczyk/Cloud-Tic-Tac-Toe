using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Application.Queries.Games.GetGameById;
using CloudTicTacToe.Application.Services;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace CloudTicTacToe.API.Hubs
{
    public class GameHub : Hub
    {
        private const string GroupNameBase = "Game";
        private const string UserConnectedResponse = "UserConnected";
        private const string BoardUpdatedResponse = "BoardUpdated";
        private readonly IGameConnectionService _connectionService;
        private readonly IMediator _mediator;

        private string GetGroupName(Guid groupId) =>
            GroupNameBase + groupId;

        public GameHub(IGameConnectionService connectionService, IMediator mediator)
        {
            _connectionService = connectionService;
            _mediator = mediator;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync(UserConnectedResponse);
        }

        public async override Task OnDisconnectedAsync(Exception? exception)
        {
            var gameId = _connectionService.GetGameByConnnectionId(Context.ConnectionId);

            if (gameId != null)
            {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, GetGroupName(gameId.Value));
                _connectionService.RemoveUserFromGame(gameId.Value, Context.ConnectionId);
                await DisplayBoard(gameId.Value);
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task AddGameConnectionId(Guid gameId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, GetGroupName(gameId));
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
                await Clients.Groups(GetGroupName(gameId)).SendAsync(BoardUpdatedResponse, gameBoardResult.Value);
            }
        }
    }
}
