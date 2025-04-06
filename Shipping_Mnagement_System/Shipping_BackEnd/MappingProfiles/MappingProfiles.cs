using AutoMapper;
using Shipping.Core.DomainModels.Identity;
using Shipping.Models;

namespace Shipping_APIs.MappingProfiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<RegisterRequest, AppUser>();
            CreateMap<AppUser, RegisterResponse>();
        }
    }
}
