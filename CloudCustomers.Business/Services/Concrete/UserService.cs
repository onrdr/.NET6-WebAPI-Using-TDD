using CloudCustomers.Business.Services.Abstract;
using CloudCustomers.Models;
using CloudCustomers.Models.Config;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Json;

namespace CloudCustomers.Business.Services.Concrete;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private readonly UsersApiOptions _apiCponfig;

    public UserService(HttpClient httpClient, IOptions<UsersApiOptions> apiCponfig)
    {
        _httpClient = httpClient;
        _apiCponfig = apiCponfig.Value;
    }

    public async Task<List<User>> GetAllUsers()
    {
        var usersResponse = await _httpClient.GetAsync(_apiCponfig.Endpoint);

        if (usersResponse.StatusCode == HttpStatusCode.NotFound)  
            return new List<User>(); 

        var responseContent = usersResponse.Content;
        var allUsers = await responseContent.ReadFromJsonAsync<List<User>>();
        return allUsers.ToList();
    }
}
