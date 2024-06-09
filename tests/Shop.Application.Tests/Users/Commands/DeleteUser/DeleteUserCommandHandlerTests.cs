using FluentAssertions;
using Moq;
using Shop.Domain.Exceptions;
using Shop.Domain.Repositories;
using Xunit;

namespace Shop.Application.Users.Commands.DeleteUser.Tests
{
    public class DeleteUserCommandHandlerTests
    {
        private readonly Mock<IUsersRepository> _usersRepositoryMock;
        private readonly DeleteUserCommandHandler _hadler;

        public DeleteUserCommandHandlerTests()
        {
            _usersRepositoryMock = new Mock<IUsersRepository>();
            _hadler = new DeleteUserCommandHandler(_usersRepositoryMock.Object);
        }

        [Fact]  
        public async Task Handle_WithValidRequest_ShouldDeleteUser()
        {
            // arrange
            var userId = "user-test-id";
            var command = new DeleteUserCommand()
            {
                Id = userId,
            };

            var user = new Domain.Entities.User()
            {
                Id = userId,
            };

            _usersRepositoryMock.Setup(u => u.GetByIdAsync(userId))
                .ReturnsAsync(user);

            _usersRepositoryMock.Setup(u => u.Delete(It.IsAny<Domain.Entities.User>()));

            // act
            await _hadler.Handle(command, CancellationToken.None);

            // assert
            _usersRepositoryMock.Verify(u => u.Delete(user), Times.Once);
        }

        [Fact]
        public async Task Handle_WithNoExistingUser_ShouldThrowNotFoundException()
        {
            // arrange
            var userId = "user-test-id";
            var request = new DeleteUserCommand()
            {
                Id = userId,
            };

            _usersRepositoryMock.Setup(u => u.GetByIdAsync(userId))
                .ReturnsAsync((Domain.Entities.User?)null);

            // act
            Func<Task> act = async () => await _hadler.Handle(request, CancellationToken.None);

            // assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"User with id: {userId} doesn't exist");
        }
    }
}