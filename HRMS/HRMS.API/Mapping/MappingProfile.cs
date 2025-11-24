using AutoMapper;
using HRMS.API.Dtos;
using HRMS.API.Models;

namespace HRMS.API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap<Employee, EmployeeResponseDto>();
            CreateMap<Employee, EmployeeResponseDto>()
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId));
            CreateMap<DepartmentCreateDto, Department>();
            CreateMap<Department, DepartmentResponseDto>();
            CreateMap<Department, DepartmentResponseDto>(); 
            
        }
    }

}