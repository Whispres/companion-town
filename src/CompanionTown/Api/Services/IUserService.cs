using Api.Models;

namespace Api.Services
{
    public interface IUserService
    {
        User CreateUser(User user);

        PagedResult<User> GetPaged(int page, int limit);
    }
}