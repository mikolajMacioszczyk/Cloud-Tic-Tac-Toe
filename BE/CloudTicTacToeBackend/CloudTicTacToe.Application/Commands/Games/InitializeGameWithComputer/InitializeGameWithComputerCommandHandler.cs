﻿using AutoMapper;
using CloudTicTacToe.Application.Helpers;
using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Application.Results;
using CloudTicTacToe.Domain.Enums;
using CloudTicTacToe.Domain.Models;
using MediatR;

namespace CloudTicTacToe.Application.Commands.Games.InitializeGameWithComputer
{
    public class InitializeGameWithComputerCommandHandler : IRequestHandler<InitializeGameWithComputerCommand, Result<GameBoardResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InitializeGameWithComputerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<GameBoardResult>> Handle(InitializeGameWithComputerCommand command, CancellationToken cancellationToken)
        {
            var player = (await _unitOfWork.PlayerRepository.GetByIDAsync(command.PlayerId));

            if (player is null)
            {
                return new NotFound(command.PlayerId);
            }

            await GameBoardHelper.ClosePlayerAciveGames(player, _unitOfWork.GameBoardRepository);

            var game = new GameBoard
            {
                PlayerX = player,
                PlayerO = await _unitOfWork.PlayerRepository.GetComputerPlayer(),
                Cells = await GameBoardHelper.GenerateCellsForBoard(GameBoard.BOARD_SIZE, _unitOfWork.CellRepository),
                State = GameBoardState.Ongoing,
            };
            game.NextPlayerId = game.PlayerX.Id;

            await _unitOfWork.GameBoardRepository.AddAsync(game);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<GameBoardResult>(game);
        }
    }
}
