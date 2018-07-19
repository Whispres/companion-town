using System.Threading.Tasks;
using Api.Models;

namespace Api.Repositories
{
    public interface IUserRepository
    {
        Task<bool> InsertAsync(User user);

        Task<User> GetAsync(string name);

        PagedResult<User> GetPaged(int skip, int limit);
    }
}