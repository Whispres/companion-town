using System.Threading.Tasks;
using Api.Models;

namespace Api.Repositories
{
    public interface IUserRepository
    {
        bool Insert(User user);

        Task<User> Get(string name);

        PagedResult<User> GetPaged(int skip, int limit);
    }
}