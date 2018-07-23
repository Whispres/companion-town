using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Repositories
{
    public interface IUserRepository
    {
        Task<bool> InsertAsync(User user);

        Task<User> GetAsync(string name);

        Task<List<User>> GetAsync();
    }
}