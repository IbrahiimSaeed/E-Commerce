using AutoMapper;
using Domain.Entites.ProductModule;
using Shared.Dtos.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    internal class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductType, TypeResultDto>();
            CreateMap<ProductBrand, BrandResultDto>();
            CreateMap<Product, ProductResultDto>()
                .ForMember(dest => dest.BrandName, options => options.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dest => dest.TypeName, options => options.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl, options => options.MapFrom<PictureUrlResolver>());
        }
    }
}
