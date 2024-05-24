using AutoMapper;
using Shop.Application.Products.Commands.CreateProduct;
using Shop.Domain.Entities;

namespace Shop.Application.Products.Dtos
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<CreateProductCommand, Product>();
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>()
                .ForMember(p => p.ProductCategory, opt => opt.MapFrom(src => src.ProductCategory));
        }
    }
}
