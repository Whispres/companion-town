using System;
using System.Threading.Tasks;
using Api.Exceptions;
using Api.Models;
using Api.Repositories;
using Api.Services;
using Api.Services.Implementation;
using AutoFixture;
using Moq;
using Xunit;

namespace Api.Tests.Services
{
    public class UserServiceTest
    {
        private readonly Fixture fixture;
        private readonly Mock<IUserRepository> userRepositoryMock;
        private readonly IUserService userService;

        public UserServiceTest()
        {
            this.fixture = new Fixture();

            this.userRepositoryMock = new Mock<IUserRepository>();

            this.userService = new UserService(this.userRepositoryMock.Object);
        }

        [Fact]
        public async Task UserService_CreateUserAsync_Valid()
        {
            // Arrange
            var user = this.fixture.Create<User>();
            this.userRepositoryMock.Setup(_ => _.InsertAsync(It.IsAny<User>())).ReturnsAsync(true);

            // Act
            var createdUser = await this.userService.CreateUserAsync(user);

            // Assert
            Assert.NotNull(createdUser);
            Assert.Equal(user.Name, createdUser.Name);
        }

        [Fact]
        public async Task UserService_CreateUserAsync_Repeated()
        {
            // Arrange
            var user = this.fixture.Create<User>();
            this.userRepositoryMock.Setup(_ => _.GetAsync(It.IsAny<string>())).ReturnsAsync(user);

            // Act
            Exception ex = await Assert.ThrowsAsync<NotModifiedException>(() => this.userService.CreateUserAsync(user));

            // Assert
            Assert.NotNull(ex);
            Assert.Equal("Already exists", ex.Message);
        }

        [Fact]
        public async Task UserService_GetAsyncAsync_ValidWithResult()
        {
            // Arrange
            var user = this.fixture.Create<User>();
            this.userRepositoryMock.Setup(_ => _.GetAsync(It.IsAny<string>())).ReturnsAsync(user);

            // Act
            var newUser = await this.userService.GetAsync(user.Identifier);

            // Assert
            Assert.NotNull(newUser);
            Assert.Equal(user.Id, newUser.Id);
        }

        [Fact]
        public async Task UserService_GetAsyncAsync_ValidWithNoResultWrongId()
        {
            // Arrange
            var user = this.fixture.Create<User>();
            this.userRepositoryMock.Setup(_ => _.GetAsync("")).ReturnsAsync(user);

            // Act
            var newUser = await this.userService.GetAsync(user.Identifier);

            // Assert
            Assert.Null(newUser);
        }

        [Fact]
        public async Task UserService_GetAsyncAsync_ValidWithNoResultNull()
        {
            // Arrange
            var user = this.fixture.Create<User>();
            this.userRepositoryMock.Setup(_ => _.GetAsync(It.IsAny<string>())).ReturnsAsync((User)null);

            // Act
            var newUser = await this.userService.GetAsync(user.Identifier);

            // Assert
            Assert.Null(newUser);
        }
    }
}