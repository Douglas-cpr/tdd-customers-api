using CloudCustomers.API.Models;

namespace CloudCustomers.UnitTests.Fixtures;

public static class UsersFixture
{
  public static List<User> GetTestUsers() => new() {
    new User {
      Id = 1,
      Name = "Username 1",
      Address = new Address()
      {
        Street = "123 Main Str",
        City = "San Francisco",
        ZipCode =  "1"
      },
      Email = "john1@example.com"
    },
    new User {
      Id = 2,
      Name = "Username 2",
      Address = new Address()
      {
        Street = "123 Main Str",
        City = "San Francisco",
        ZipCode =  "2"
      },
      Email = "john2@example.com"
    },
    new User {
      Id = 3,
      Name = "Username 3",
      Address = new Address()
      {
        Street = "123 Main Str",
        City = "San Francisco",
        ZipCode =  "3"
      },
      Email = "john3ß@example.com"
    }
  };
}