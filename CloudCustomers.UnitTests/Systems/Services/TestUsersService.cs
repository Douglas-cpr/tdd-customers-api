using CloudCustomers.API.Models;
using CloudCustomers.Services;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using Moq.Protected;

namespace CloudCustomers.UnitTests.Systems.Services;

public class TestUsersService 
{
  [Fact]
  public async Task GetAllUsers_WhenCalled_InvokesHTTPGetRequest() 
  {
    var expectedResponse = UsersFixture.GetTestUsers();
    var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
    var httpClient = new HttpClient(handlerMock.Object);
    var sut = new UsersService(httpClient);
    await sut.GetAllUsers();

    handlerMock
      .Protected()
      .Verify(
        "SendAsync",
        Times.Exactly(1),
        ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
        ItExpr.IsAny<CancellationToken>()
      );
  }

  [Fact]
  public async Task GetAllUsers_WhenCalled_ReturnsListOfUsers() {
    var expectedResponse = UsersFixture.GetTestUsers();
    var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
    var httpClient = new HttpClient(handlerMock.Object);
    var sut = new UsersService(httpClient);

    var result = await sut.GetAllUsers();

    result.Should().BeOfType<List<User>>();
  }
}