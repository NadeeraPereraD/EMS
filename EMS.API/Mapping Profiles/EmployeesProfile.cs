using AutoMapper;
using EMS.API.Models.DTOs;
using EMS.API.Models.Entities;

namespace EMS.API.Mapping_Profiles
{
    public class EmployeesProfile : Profile
    {
        public EmployeesProfile() 
        {
            CreateMap<Employee, EmployeesDto>()
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.name));
        }
    }
}
