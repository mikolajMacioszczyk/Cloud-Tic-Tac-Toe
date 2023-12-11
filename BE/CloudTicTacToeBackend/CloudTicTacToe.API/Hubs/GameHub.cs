using AutoMapper;
using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Application.Results;
using CloudTicTacToe.Application.Services;
using CloudTicTacToe.Domain.Models;
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
        private readonly IMapper _mapper;

        public GameHub(GameConnectionService connectionService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _connectionService = connectionService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

        // TODO: TurnPlayed

        public async Task DisplayBoard(Guid gameId)
        {
            // TODO: move to some service => as no tracking
            var gameBoard = await  _unitOfWork.GameBoardRepository.GetByIDAsync(gameId, $"{nameof(GameBoard.PlayerX)},{nameof(GameBoard.PlayerO)},{nameof(GameBoard.Cells)}");
            if (gameBoard != null)
            {
                await Clients.Groups(GroupName).SendAsync(BoardUpdatedResponse, _mapper.Map<GameBoardResult>(gameBoard));
            }
        }
    }
}
