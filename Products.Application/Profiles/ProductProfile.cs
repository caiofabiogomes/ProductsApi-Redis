using AutoMapper;
using Products.Application.ViewModels;
using Products.Core.Entites;

namespace Products.Application.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
        }
    }
}
