using AutoMapper;
using IDP.Api.Models;
using IDP.Api.Dtos;

namespace IDP.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User mappings
            CreateMap<User, UserResponseDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role != null ? src.Role.Name : string.Empty));

            CreateMap<RegisterDto, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()) // Will be set manually after hashing
                .ForMember(dest => dest.Role, opt => opt.Ignore()); // Will be loaded from database

            // Role mappings
            CreateMap<Role, RoleResponseDto>();
        }
    }
}

