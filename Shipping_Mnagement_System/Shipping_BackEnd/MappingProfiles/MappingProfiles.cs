using AutoMapper;
using Shipping.Core.Models;
using Shipping.Models;

namespace Shipping_APIs.MappingProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee, EmployeeToReturn>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AppUser.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.AppUser.PhoneNumber))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.AppUser.Address));

            CreateMap<Merchant, MerchantToReturn>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AppUser.FullName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.AppUser.PhoneNumber))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.AppUser.Address));

            CreateMap<DeliveryMan, DeliveryManToReturn>()
               .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.AppUser.FullName))
               .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email))
               .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.AppUser.PhoneNumber))
               .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.AppUser.Address));


        }
    }
}
