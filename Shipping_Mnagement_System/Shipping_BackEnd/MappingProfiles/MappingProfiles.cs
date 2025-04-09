using AutoMapper;
using Shipping.Core.DomainModels;
using Shipping.Core.DomainModels.Identity;
using Shipping.Core.DomainModels.OrderModels;
using Shipping.Models;

namespace Shipping_APIs.MappingProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RegisterRequest, AppUser>();
            CreateMap<AppUser, RegisterResponse>();

            CreateMap<Merchant, MerchantDto>();

            CreateMap<DeliveryMan, DeliveryManDto>()
                .ForMember(dest => dest.DiscountType, opt => opt.MapFrom(src => src.DiscountType.ToString()));

            CreateMap<PaymentMethod, PaymentMethodDto>();

            CreateMap<ShippingType, ShippingTypeDto>();

            CreateMap<Branch, BranchDto>();

            CreateMap<Order, LocationDto>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src =>
                    $"{src.Area.Name}, {src.City.Name}, {src.Governorate.Name}"));

            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.DeliveryMan, opt => opt.MapFrom(src => src.DeliveryAgent))
                .ForMember(dest => dest.DeliveryLocation, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.Branch));
        }
    }
}
