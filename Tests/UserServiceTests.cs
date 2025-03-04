using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BankingSystemAPI.DTOs;
using BankingSystemAPI.Models;
using BankingSystemAPI.Repositories;
using BankingSystemAPI.Services;
using Moq;
using Xunit;

namespace BankingSystemAPI.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UserService _userService;

public UserServiceTests()
{
    _userRepositoryMock = new Mock<IUserRepository>();
    _mapperMock = new Mock<IMapper>();
    _userService = new UserService(_userRepositoryMock.Object, _mapperMock.Object);
}

        [Fact]
        public async Task GetAllUsersAsync_ReturnsUserDtos()
        {
            var users = new List<User> { new User { UserId = 1, FullName = "John Doe" } };
            var userDtos = new List<UserDto> { new UserDto { UserId = 1, FullName = "John Doe" } };

            _userRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<UserDto>>(users)).Returns(userDtos);


            var result = await _userService.GetAllUsersAsync();


            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(userDtos, result);
        }

    }
}