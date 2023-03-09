
using CloudCustomers.Models;

namespace CloudCustomers.UnitTests.Fixtures;

internal static class UsersFixture
{
    internal static List<User> GetTestUsers() => new()
    {
        new User
        {
            Id = 1,
            Name = "Test User 1",
            Email = "test.user1@email.com",
            Address = new()
            {
                Street = "Test Street 1",
                City = "Test Street 1",
                ZipCode = "TestZipCode1"
            }
        },
        new User
        {
            Id = 2,
            Name = "Test User 2",
            Email = "test.user2@email.com",
            Address = new()
            {
                Street = "Test Street 2",
                City = "Test Street 2",
                ZipCode = "TestZipCode2"
            }
        },
        new User
        {
            Id = 3,
            Name = "Test User 3",
            Email = "test.user3@email.com",
            Address = new()
            {
                Street = "Test Street 3",
                City = "Test Street 3", 
                ZipCode = "TestZipCode3"
            }
        },
    };
}
