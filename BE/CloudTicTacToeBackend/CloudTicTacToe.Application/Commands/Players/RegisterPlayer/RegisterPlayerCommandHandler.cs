using AutoMapper;
using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Application.Results;
using CloudTicTacToe.Domain.Models;
using MediatR;

namespace CloudTicTacToe.Application.Commands.Players.RegisterPlayer
{
    public class RegisterPlayerCommandHandler : IRequestHandler<RegisterPlayerCommand, Result<PlayerResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterPlayerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PlayerResult>> Handle(RegisterPlayerCommand command, CancellationToken cancellationToken)
        {
            var player = new Player() { Name = command.Name, IsComputer = false };

            await _unitOfWork.PlayerRepository.AddAsync(player);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<PlayerResult>(player);
        }
    }
}
