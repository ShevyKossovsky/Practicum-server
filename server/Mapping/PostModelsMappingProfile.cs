using AutoMapper;
using server.Models;
using Server.Core.Entities;

namespace server.Mapping
{
    public class PostModelsMappingProfile : Profile
    {
        public PostModelsMappingProfile()
        {

            CreateMap<EmployeePostModel, Employee>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => Enum.Parse<Gender>(src.Gender, true)));

            CreateMap<EmployeePositionPostModel, EmployeePosition>();

            CreateMap<PositionPostModel, Position>();

        }
    }
}