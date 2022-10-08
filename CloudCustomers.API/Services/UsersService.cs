using CloudCustomers.API.Models;

namespace CloudCustomers.Services;


public interface IUsersService
{
  public Task<List<User>> GetAllUsers();
}

public class UsersService : IUsersService 
{
  private readonly HttpClient _httpClient;

   public UsersService(HttpClient httpClient) 
   { 
    _httpClient = httpClient;
   }

  public async Task<List<User>> GetAllUsers()
  {
    var usersReponse = await _httpClient.GetAsync("https://test.com");
    return new List<User>{};
  }
}