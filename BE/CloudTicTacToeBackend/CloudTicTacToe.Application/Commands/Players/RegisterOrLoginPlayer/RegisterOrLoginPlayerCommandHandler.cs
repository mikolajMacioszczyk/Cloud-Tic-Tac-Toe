using AutoMapper;
using CloudTicTacToe.Application.Interfaces;
using CloudTicTacToe.Application.Models;
using CloudTicTacToe.Application.Results;
using CloudTicTacToe.Domain.Models;
using MediatR;

namespace CloudTicTacToe.Application.Commands.Players.RegisterOrLoginPlayer
{
    public class RegisterOrLoginPlayerCommandHandler : IRequestHandler<RegisterOrLoginPlayerCommand, Result<PlayerResult>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegisterOrLoginPlayerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PlayerResult>> Handle(RegisterOrLoginPlayerCommand command, CancellationToken cancellationToken)
        {
            var existing = await _unitOfWork.PlayerRepository.GetFirstOrDefaultAsync(p => p.Name == command.Name);

            if (existing is {})
            {
                return _mapper.Map<PlayerResult>(existing);
            }

            var player = new Player() { Name = command.Name, IsComputer = false };

            await _unitOfWork.PlayerRepository.AddAsync(player);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<PlayerResult>(player);
        }
    }
}
