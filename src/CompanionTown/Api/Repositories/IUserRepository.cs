using Api.Models;

namespace Api.Repositories
{
    public interface IUserRepository
    {
        bool Insert(User user);

        User Get(string name);

        PagedResult<User> GetPaged(int skip, int limit);
    }
}