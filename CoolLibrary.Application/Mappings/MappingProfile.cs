using AutoMapper;
using CoolLibrary.Application.DTO;
using CoolLibrary.Domain.Entities;
using CoolLibrary.Domain.Enums;

namespace CoolLibrary.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Author Mappings
        CreateMap<Author, AuthorDTO>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}".Trim()))
            .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.BookAuthors.Select(ba => ba.Book)));

        CreateMap<Book, AuthorBookDTO>();

        // Book Mappings
        CreateMap<Book, BookDTO>()
            .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.IsAvailable));

        // Customer Mappings (Output)
        CreateMap<Customer, CustomerDTO>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}".Trim()))
            .ForMember(dest => dest.MembershipStatus, opt => opt.MapFrom(src => src.MembershipStatus.ToString()));

        // Customer Mappings (Input - Create)
        CreateMap<CreateCustomerDTO, Customer>()
            .ForMember(dest => dest.MembershipDate, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.MembershipStatus, opt => opt.MapFrom(src => MembershipStatus.Active))
            .ForMember(dest => dest.MaxBooksAllowed, opt => opt.MapFrom(src => src.MaxBooksAllowed ?? 5))
            .ForMember(dest => dest.CustomerId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Loans, opt => opt.Ignore())
            .ForMember(dest => dest.Reservations, opt => opt.Ignore())
            .ForMember(dest => dest.Fines, opt => opt.Ignore());
            
        // Customer Mappings (Input - Update)
        CreateMap<UpdateCustomerDTO, Customer>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            
        CreateMap<Customer, UpdateCustomerDTO>();
    }
}