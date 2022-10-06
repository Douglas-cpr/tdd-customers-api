using CloudCustomers.API.Controllers;
using CloudCustomers.API.Models;
using CloudCustomers.Services;
using CloudCustomers.UnitTests.Fixtures;

namespace CloudCustomers.UnitTests;

public class TestUsersController
{
    [Fact]
    public async Task Get_OnSuccess_ReturnsStatusCode200()
    {
        var mockUserService = new Mock<IUserService>();
        mockUserService 
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers());
        var sut = new UsersController(mockUserService.Object);

        var result = (OkObjectResult)await sut.Get();

        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_OnSuccess_InvokesUserServiceExactlyOnce() 
    {
        var mockUserService = new Mock<IUserService>();
        mockUserService 
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());
        var sut = new UsersController(mockUserService.Object);

        await sut.Get();

        mockUserService.Verify(
            service => service.GetAllUsers(), 
            Times.Once()
        );
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnsListOfUsers()
    {
        var mockUserService = new Mock<IUserService>();
        mockUserService 
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(UsersFixture.GetTestUsers());
        var sut = new UsersController(mockUserService.Object);

        var result = await sut.Get();

        result.Should().BeOfType<OkObjectResult>();
        var objectResult = (OkObjectResult)result;
        objectResult.Value.Should().BeOfType<List<User>>(); 

    }

    [Fact]
    public async Task Get_OnNoUsersFound_Returns404()
    {
        var mockUserService = new Mock<IUserService>();
        mockUserService 
            .Setup(service => service.GetAllUsers())
            .ReturnsAsync(new List<User>());
        var sut = new UsersController(mockUserService.Object);

        var result = await sut.Get();

        result.Should().BeOfType<NotFoundResult>();
        var objectResult = (NotFoundResult)result;
        objectResult.StatusCode.Should().Be(404);
    }
}