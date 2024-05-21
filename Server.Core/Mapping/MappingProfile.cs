using AutoMapper;
using Server.Core.DTOs;
using Server.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core.Mapping
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()));
            CreateMap<Position, PositionDto>().ReverseMap();
            CreateMap<EmployeePosition, EmployeePositionDto>().ReverseMap();
            

        }
    }
}
