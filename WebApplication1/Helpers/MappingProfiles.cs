using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(P => P.ProductType, O => O.MapFrom(S => S.ProductType.Name))
                .ForMember(P => P.ProductBrand, O => O.MapFrom(S => S.ProductBrand.Name))
                .ForMember(P=>P.PictureUrl , O=>O.MapFrom<ProductPictureUrlResolver>());
        }
    }
}
