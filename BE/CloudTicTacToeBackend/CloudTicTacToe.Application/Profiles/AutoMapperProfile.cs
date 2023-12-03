using AutoMapper;
using CloudTicTacToe.Application.Commands.Games.Results;
using CloudTicTacToe.Application.Profiles.Resolvers;
using CloudTicTacToe.Domain.Models;

namespace CloudTicTacToe.Application.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Cell, CellResult>();
            CreateMap<GameBoard, GameBoardResult>()
                .ForMember(m => m.Board, opt => opt.MapFrom<BoardResolver>());
        }
    }
}
