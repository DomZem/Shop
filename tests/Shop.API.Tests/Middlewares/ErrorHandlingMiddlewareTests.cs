using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Shop.Domain.Entities;
using Shop.Domain.Exceptions;
using Xunit;

namespace Shop.API.Middlewares.Tests
{
    public class ErrorHandlingMiddlewareTests
    {
        [Fact]
        public async Task InvokeAsync_WhenNoExceptionThrown_ShouldCallNextDelegate()
        {
            // arrange
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var context = new DefaultHttpContext();
            var nextDeletegateMock = new Mock<RequestDelegate>();

            // act
            await middleware.InvokeAsync(context, nextDeletegateMock.Object);

            // assert
            nextDeletegateMock.Verify(next => next.Invoke(context), Times.Once);
        }

        [Fact]
        public async Task InvokeAsync_WhenExistingUserExceptionThrown_ShouldSetStatuCode409()
        {
            // arrange
            var context = new DefaultHttpContext();
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var existingUserException = new ExistingUserException("test@gmail.com");

            // act
            await middleware.InvokeAsync(context, _ => throw existingUserException);

            // assert
            context.Response.StatusCode.Should().Be(409);
        }

        [Fact]
        public async Task InvokeAsync_WhenNotFoundExceptionThrown_ShouldSetStatusCode404()
        {
            // arrange
            var context = new DefaultHttpContext();
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var notFoundException = new NotFoundException(nameof(Product), "1");

            // act
            await middleware.InvokeAsync(context, _ => throw notFoundException);

            // assert
            context.Response.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task InvokeAsync_WhenForbiddenExceptionThrown_ShouldSetStatusCode403()
        {
            // arrange
            var context = new DefaultHttpContext();
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var forbiddenException = new ForbiddenException();

            // act
            await middleware.InvokeAsync(context, _ => throw forbiddenException);

            // assert
            context.Response.StatusCode.Should().Be(403);
        }

        [Fact]
        public async Task InvokeAsync_WhenProductUnavailableExceptionThrown_ShouldSetStatusCode409()
        {
            // arrange
            var context = new DefaultHttpContext();
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var productUnavailableException = new ProductUnavailableException();

            // act
            await middleware.InvokeAsync(context, _ => throw productUnavailableException);

            // assert
            context.Response.StatusCode.Should().Be(409);
        }

        [Fact]
        public async Task InvokeAsync_WhenGenericExceptionThrown_ShouldSetStatusCode500()
        {
            // arrange
            var context = new DefaultHttpContext();
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var exception = new Exception();

            // act
            await middleware.InvokeAsync(context, _ => throw exception);

            // assert
            context.Response.StatusCode.Should().Be(500);
        }
    }
}