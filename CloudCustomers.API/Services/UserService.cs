using CloudCustomers.API.Models;

namespace CloudCustomers.Services;


public interface IUserService
{
  public Task<List<User>> GetAllUsers();
}

public class UserService : IUserService 
{
   public UserService() { }

  public Task<List<User>> GetAllUsers()
  {
    throw new NotImplementedException();
  }
}