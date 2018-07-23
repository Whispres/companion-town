using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Services
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(User user);

        Task<List<User>> GetAsync();

        Task<User> GetAsync(string id);
    }
}