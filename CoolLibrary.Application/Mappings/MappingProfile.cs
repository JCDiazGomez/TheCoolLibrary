using AutoMapper;
using CoolLibrary.Application.DTO;
using CoolLibrary.Domain.Entities;

namespace CoolLibrary.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Author Mappings
        CreateMap<Author, AuthorDTO>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}".Trim()));

        // Book Mappings
        CreateMap<Book, BookDTO>()
            .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.IsAvailable));

        // Customer Mappings
        CreateMap<Customer, CustomerDTO>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}".Trim()))
            .ForMember(dest => dest.MembershipStatus, opt => opt.MapFrom(src => src.MembershipStatus.ToString()));
    }
}