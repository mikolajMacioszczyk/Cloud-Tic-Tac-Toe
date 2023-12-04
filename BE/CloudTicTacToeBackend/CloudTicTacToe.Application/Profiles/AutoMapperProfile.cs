using AutoMapper;
using CloudTicTacToe.Application.Profiles.Resolvers;
using CloudTicTacToe.Application.Results;
using CloudTicTacToe.Domain.Models;

namespace CloudTicTacToe.Application.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Player, PlayerResult>();
            CreateMap<Cell, CellResult>();
            CreateMap<GameBoard, GameBoardResult>()
                .ForMember(m => m.Board, opt => opt.MapFrom<BoardResolver>());
        }
    }
}
