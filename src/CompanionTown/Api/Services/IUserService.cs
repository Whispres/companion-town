using System.Threading.Tasks;
using Api.Models;

namespace Api.Services
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(User user);

        PagedResult<User> GetPaged(int page, int limit);

        Task<User> GetAsync(string id);
    }
}