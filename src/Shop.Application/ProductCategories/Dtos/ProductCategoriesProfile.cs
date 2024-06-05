using AutoMapper;
using Shop.Application.ProductCategories.Commands.CreateProductCategory;
using Shop.Application.ProductCategories.Commands.UpdateProductCategory;
using Shop.Domain.Entities;

namespace Shop.Application.ProductCategories.Dtos
{
    public class ProductCategoriesProfile : Profile
    {
        public ProductCategoriesProfile()
        {
            CreateMap<ProductCategory, ProductCategoryDto>();
            CreateMap<UpdateProductCategoryCommand, ProductCategory>();
            CreateMap<CreateProductCategoryCommand, ProductCategory>();
        }
    }
}
