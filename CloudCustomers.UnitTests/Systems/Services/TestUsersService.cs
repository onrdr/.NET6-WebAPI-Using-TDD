
using CloudCustomers.Business.Services.Concrete;
using CloudCustomers.Models;
using CloudCustomers.Models.Config;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions; 
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected; 

namespace CloudCustomers.UnitTests.Systems.Services;

public class TestUsersService
{
    [Fact]
    public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequests()
    {
        // Arrange
        var expectedResponse = UsersFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        var endpoint = "https://example.com";
        var config = Options.Create(new UsersApiOptions { Endpoint = endpoint });
        var sut = new UserService(httpClient, config);

        // Action
        await sut.GetAllUsers();

        // Assert
        handlerMock
            .Protected()
            .Verify(    
                "SendAsync", 
                Times.Exactly(1), 
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task GetAllUsers_WhenHits404_ReturnsEmptyListOfUsers()
    {
        // Arrange 
        var handlerMock = MockHttpMessageHandler<User>.SetupReturns404();
        var httpClient = new HttpClient(handlerMock.Object);
        var endpoint = "https://example.com";
        var config = Options.Create(new UsersApiOptions { Endpoint = endpoint });
        var sut = new UserService(httpClient, config);

        // Action
        var result = await sut.GetAllUsers();

        // Assert
        result.Count.Should().Be(0);
    }

    [Fact]
    public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
    {
        // Arrange 
        var expectedResponse = UsersFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        var endpoint = "https://example.com";
        var config = Options.Create(new UsersApiOptions { Endpoint = endpoint });
        var sut = new UserService(httpClient, config);

        // Action
        var result = await sut.GetAllUsers();

        // Assert
        result.Count.Should().Be(expectedResponse.Count);
    }

    [Fact]
    public async Task GetAllUsers_WhenCalled_InvokesConfiguredExternalUrl()
    {
        // Arrange 
        var expectedResponse = UsersFixture.GetTestUsers();
        var endpoint = "https://example.com/users";
        var handlerMock = MockHttpMessageHandler<User>
            .SetupBasicGetResourceList(expectedResponse, endpoint);

        var httpClient = new HttpClient(handlerMock.Object);

        var config = Options.Create(new UsersApiOptions { Endpoint = endpoint });

        var sut = new UserService(httpClient, config);

        // Action
        var result = await sut.GetAllUsers();

        var uri = new Uri(endpoint);

        // Assert
        handlerMock
            .Protected()
            .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(
                    req => req.Method == HttpMethod.Get &&
                    req.RequestUri == uri),
                ItExpr.IsAny<CancellationToken>());         
    }
}
