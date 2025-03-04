using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using BankingApp.Controllers;
using BankingSystemAPI.Services;
using BankingSystemAPI.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

public class UserControllerTests
{
    private readonly Mock<IUserService> _mockUserService;
    private readonly Mock<ILogger<UserController>> _mockLogger;
    private readonly UserController _controller;

    public UserControllerTests()
    {
        _mockUserService = new Mock<IUserService>();
        _mockLogger = new Mock<ILogger<UserController>>();
        _controller = new UserController(_mockUserService.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetUsers_ReturnsOk_WhenUsersExist()
    {
        var users = new List<UserDto> { new UserDto { UserId = 1} };
        _mockUserService.Setup(s => s.GetAllUsersAsync()).ReturnsAsync(users);
        
        var result = await _controller.GetUsers();
        
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedUsers = Assert.IsType<List<UserDto>>(okResult.Value);
        Assert.Single(returnedUsers);
    }

    [Fact]
    public async Task GetUsers_ReturnsNotFound_WhenNoUsersExist()
    {
        _mockUserService.Setup(s => s.GetAllUsersAsync()).ReturnsAsync(new List<UserDto>());
        
        var result = await _controller.GetUsers();
        
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetUser_ReturnsOk_WhenUserExists()
    {
        var user = new UserDto { UserId = 1};
        _mockUserService.Setup(s => s.GetUserByIdAsync(1)).ReturnsAsync(user);
        
        var result = await _controller.GetUser(1);
        
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedUser = Assert.IsType<UserDto>(okResult.Value);
        Assert.Equal(1, returnedUser.UserId);
    }

    [Fact]
    public async Task GetUser_ReturnsNotFound_WhenUserDoesNotExist()
    {
        _mockUserService.Setup(s => s.GetUserByIdAsync(1)).ReturnsAsync((UserDto)null);
        
        var result = await _controller.GetUser(1);
        
        Assert.IsType<NotFoundObjectResult>(result.Result);
    }

    [Fact]
    public async Task PostUser_ReturnsCreatedAtAction_WhenUserIsCreated()
    {
        var userDto = new UserDto { UserId = 1};
        _mockUserService.Setup(s => s.AddUserAsync(userDto)).ReturnsAsync(userDto);
        
        var result = await _controller.PostUser(userDto);
        
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnedUser = Assert.IsType<UserDto>(createdAtActionResult.Value);
        Assert.Equal(1, returnedUser.UserId);
    }

    [Fact]
    public async Task PutUser_ReturnsBadRequest_WhenIdMismatch()
    {
        var userDto = new UserDto { UserId = 1};
        
        var result = await _controller.PutUser(2, userDto);
        
        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task PutUser_ReturnsNoContent_WhenUserIsUpdated()
    {
        var userDto = new UserDto { UserId = 1 };
        _mockUserService.Setup(s => s.UpdateUserAsync(1, userDto)).Returns(Task.CompletedTask);
        
        var result = await _controller.PutUser(1, userDto);
        
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteUser_ReturnsNotFound_WhenUserDoesNotExist()
    {
        _mockUserService.Setup(s => s.GetUserByIdAsync(1)).ReturnsAsync((UserDto)null);
        
        var result = await _controller.DeleteUser(1);
        
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task DeleteUser_ReturnsNoContent_WhenUserIsDeleted()
    {
        var userDto = new UserDto { UserId = 1};
        _mockUserService.Setup(s => s.GetUserByIdAsync(1)).ReturnsAsync(userDto);
        _mockUserService.Setup(s => s.DeleteUserAsync(1)).Returns(Task.CompletedTask);
        
        var result = await _controller.DeleteUser(1);
        
        Assert.IsType<NoContentResult>(result);
    }
}
