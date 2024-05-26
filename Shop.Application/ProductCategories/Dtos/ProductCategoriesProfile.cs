using AutoMapper;
using Shop.Application.ProductCategories.Commands.CreateProductCategory;
using Shop.Domain.Entities;

namespace Shop.Application.ProductCategories.Dtos
{
    public class ProductCategoriesProfile : Profile
    {
        public ProductCategoriesProfile()
        {
            CreateMap<ProductCategory, ProductCategoryDto>();

            CreateMap<CreateProductCategoryCommand, ProductCategory>();
        }
    }
}
