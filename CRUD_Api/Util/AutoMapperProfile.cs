using AutoMapper;
using CRUD_Api.DTO;
using CRUD_Api.Models;

namespace CRUD_Api.Util;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserAddDto, User>()
            .ForMember(x => x.FirstName,
                opt => opt.MapFrom(src => src.FirstName))
            .ForMember(x => x.LastName,
                opt => opt.MapFrom(src => src.LastName))
            .ForMember(x => x.Age,
                opt => opt.MapFrom(src => src.Age))
            .ForMember(x => x.Email,
                opt => opt.MapFrom(src => src.Email));
        
        CreateMap<UserUpdateDto, User>()
            .ForMember(x => x.FirstName,
                opt => opt.MapFrom(src => src.FirstName))
            .ForMember(x => x.LastName,
                opt => opt.MapFrom(src => src.LastName))
            .ForMember(x => x.Age,
                opt => opt.MapFrom(src => src.Age))
            .ForMember(x => x.Email,
                opt => opt.MapFrom(src => src.Email));

    }
}