using AutoMapper;
using FluentAssertions;
using Moq;
using Shop.Domain.Entities;
using Shop.Domain.Exceptions;
using Shop.Domain.Repositories;
using Xunit;

namespace Shop.Application.ProductCategories.Commands.UpdateProductCategory.Tests
{
    public class UpdateProductCategoryCommandHandlerTests
    {
        private readonly Mock<IProductCategoriesRepository> _productCategoriesRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly UpdateProductCategoryCommandHandler _handler;

        public UpdateProductCategoryCommandHandlerTests()
        {
            _productCategoriesRepositoryMock = new Mock<IProductCategoriesRepository>();
            _mapperMock = new Mock<IMapper>();

            _handler = new UpdateProductCategoryCommandHandler(_productCategoriesRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_WithValidRequest_ShouldUpdateProductCategory()
        {
            // arrange
            var productCategoryId = 1;
            var command = new UpdateProductCategoryCommand()
            {
                Id = productCategoryId,
                Name = "Test product category"
            };

            var productCategory = new ProductCategory()
            {
                Id = productCategoryId,
            };

            _productCategoriesRepositoryMock.Setup(o => o.GetByIdAsync(productCategoryId))
                .ReturnsAsync(productCategory);

            // act
            await _handler.Handle(command, CancellationToken.None);

            // assert
            _productCategoriesRepositoryMock.Verify(o => o.SaveChanges(), Times.Once);
            _mapperMock.Verify(m => m.Map(command, productCategory), Times.Once);
        }

        [Fact]
        public async Task Handle_WithNoExistingProductCategory_ShouldThrowNotFoundException()
        {
            // arrange
            var productCategoryId = 2;
            var request = new UpdateProductCategoryCommand()
            {
                Id = productCategoryId,
            };

            _productCategoriesRepositoryMock.Setup(p => p.GetByIdAsync(productCategoryId))
                .ReturnsAsync((ProductCategory?)null);

            // act
            Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

            // assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Product category with id: {productCategoryId} doesn't exist");
        }
    }
}