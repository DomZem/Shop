using AutoMapper;
using Shop.Domain.Entities;

namespace Shop.Application.ProductCategories.Dtos
{
    public class ProductCategoriesProfile : Profile
    {
        public ProductCategoriesProfile()
        {
            CreateMap<ProductCategory, ProductCategoryDto>();
        }
    }
}
