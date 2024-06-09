using FluentAssertions;
using Moq;
using Shop.Domain.Entities;
using Shop.Domain.Exceptions;
using Shop.Domain.Repositories;
using Xunit;

namespace Shop.Application.Products.Commands.DeleteProduct.Tests
{
    public class DeleteProductCommandHandlerTests
    {
        private readonly Mock<IProductsRepository> _productsRepositoryMock;
        private readonly DeleteProductCommandHandler _handler;

        public DeleteProductCommandHandlerTests()
        {
            _productsRepositoryMock = new Mock<IProductsRepository>();
            _handler = new DeleteProductCommandHandler(_productsRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_WithValidRequest_ShouldDeleteProduct()
        {
            // arrange
            var productId = 1;
            var command = new DeleteProductCommand()
            {
                Id = productId,
            };

            var product = new Product()
            {
                Id = productId,
            };

            _productsRepositoryMock.Setup(p => p.GetByIdAsync(productId))
                .ReturnsAsync(product);
            _productsRepositoryMock.Setup(p => p.Delete(It.IsAny<Product>()));

            // act
            await _handler.Handle(command, CancellationToken.None);

            // assert
            _productsRepositoryMock.Verify(p => p.Delete(product), Times.Once);
        }

        [Fact]
        public async Task Handle_WithNoExistingProduct_ShouldThrowNotFoundException()
        {
            // arrange
            var productId = 2;
            var request = new DeleteProductCommand()
            {
                Id = productId,
            };

            _productsRepositoryMock.Setup(p => p.GetByIdAsync(productId))
                .ReturnsAsync((Product?)null);

            // act
            Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

            // assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Product with id: {productId} doesn't exist");
        }
    }
}