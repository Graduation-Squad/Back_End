using AutoMapper;
using Shipping.Core.Models;
using Shipping.Core.Models.Identity;
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
