using AutoMapper;
using FluentAssertions;
using Moq;
using Shop.Domain.Entities;
using Shop.Domain.Exceptions;
using Shop.Domain.Repositories;
using Xunit;

namespace Shop.Application.Products.Commands.UpdateProduct.Tests
{
    public class UpdateProductCommandHandlerTests
    {
        private readonly Mock<IProductsRepository> _productsRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly UpdateProductCommandHandler _handler;

        public UpdateProductCommandHandlerTests()
        {
            _productsRepositoryMock = new Mock<IProductsRepository>();  
            _mapperMock = new Mock<IMapper>();

            _handler = new UpdateProductCommandHandler(_productsRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_WithValidRequest_ShouldUpdateProduct()
        {
            // arrange
            var productId = 1;
            var comamnd = new UpdateProductCommand()
            {
                Id = productId,
            };

            var product = new Product()
            {
                Id = productId,
            };

            _productsRepositoryMock.Setup(p => p.GetByIdAsync(productId))
                .ReturnsAsync(product);

            // act
            await _handler.Handle(comamnd, CancellationToken.None);

            // assert
            _productsRepositoryMock.Verify(p => p.SaveChanges(), Times.Once);
            _mapperMock.Verify(m => m.Map(comamnd, product), Times.Once);
        }

        [Fact]  
        public async Task Handle_WithNoExistingProduct_ShouldThrowNotFoundException()
        {
            // arrange
            var productId = 2;
            var request = new UpdateProductCommand()
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