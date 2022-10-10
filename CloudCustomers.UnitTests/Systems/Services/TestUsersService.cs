using CloudCustomers.API.Config;
using CloudCustomers.API.Models;
using CloudCustomers.Services;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using Microsoft.Extensions.Options;
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
    var endpoint = "https://examples.com/users";
    var config = Options.Create(new UsersApiOptions {
      Endpoint =  endpoint
    });
    var sut = new UsersService(httpClient, config);
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
    var endpoint = "https://examples.com/users";
    var httpClient = new HttpClient(handlerMock.Object);
        var config = Options.Create(new UsersApiOptions {
      Endpoint =  endpoint
    });
    var sut = new UsersService(httpClient, config);

    var result = await sut.GetAllUsers();

    result.Should().BeOfType<List<User>>();
  }

  [Fact]
  public async Task GetAllUsers_WhenHits404_ReturnsEmptyListOfUsers() {
    var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
    var endpoint = "https://examples.com/users";
    var httpClient = new HttpClient(handlerMock.Object);
    var config = Options.Create(new UsersApiOptions {
      Endpoint =  endpoint
    });
    var sut = new UsersService(httpClient, config);

    var result = await sut.GetAllUsers();

    result.Count.Should().Be(0);
  }

  [Fact]
  public async Task GetAllUsers_WhenCalled_Returns404WhenNoHaveUsersOfExpectedSize() {
    var expectedResponse = UsersFixture.GetTestUsers();
    var endpoint = "https://examples.com/users";
    var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
    var httpClient = new HttpClient(handlerMock.Object);
    var config = Options.Create(new UsersApiOptions {
      Endpoint =  endpoint
    });
    var sut = new UsersService(httpClient, config);

    var result = await sut.GetAllUsers();

    result.Count.Should().Be(expectedResponse.Count);
  }

  [Fact]
  public async Task GetAllUsers_WhenCalled_InvokesConfigureExternalUrl() {
    var expectedResponse = UsersFixture.GetTestUsers(); 
    var endpoint = "https://examples.com/users";
    var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse, endpoint);
    var httpClient = new HttpClient(handlerMock.Object);
    var config = Options.Create(new UsersApiOptions {
      Endpoint =  endpoint
    });
    var sut = new UsersService(httpClient, config);
 
    var result = await sut.GetAllUsers();

    handlerMock
      .Protected()
      .Verify(
        "SendAsync",
        Times.Exactly(1), 
        ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get 
        && req.RequestUri.ToString() == endpoint),
        ItExpr.IsAny<CancellationToken>()
      );

    result.Count.Should().Be(expectedResponse.Count);
  }
}