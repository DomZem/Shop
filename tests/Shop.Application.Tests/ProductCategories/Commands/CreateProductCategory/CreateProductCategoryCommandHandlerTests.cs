using AutoMapper;
using FluentAssertions;
using Moq;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;
using Xunit;

namespace Shop.Application.ProductCategories.Commands.CreateProductCategory.Tests
{
    public class CreateProductCategoryCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ForValidCommand_ReturnsCreatedProductCategoryId()
        {
            // arrange
            var mapperMock = new Mock<IMapper>();

            var command = new CreateProductCategoryCommand();
            var productCategory = new ProductCategory()
            {
                Name = "Fashion",
            };
            mapperMock.Setup(m => m.Map<ProductCategory>(command))
                .Returns(productCategory);

            var productCategoriesRepositoryMock = new Mock<IProductCategoriesRepository>();
            productCategoriesRepositoryMock.Setup(repo => repo.Create(It.IsAny<ProductCategory>()))
                .ReturnsAsync(1);

            var commandHandler = new CreateProductCategoryCommandHandler(productCategoriesRepositoryMock.Object, mapperMock.Object);

            // act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // assert
            result.Should().Be(1);
            productCategoriesRepositoryMock.Verify(r => r.Create(productCategory), Times.Once);
        }
    }
}