using CloudCustomers.Models;

namespace CloudCustomers.Business.Services.Abstract;

public interface IUserService
{
    Task<List<User>> GetAllUsers();
}
