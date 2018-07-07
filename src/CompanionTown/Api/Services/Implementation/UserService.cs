using Api.Models;
using Api.Repositories;

namespace Api.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public User CreateUser(User user)
        {
            if (_userRepository.Get(user.Name) != null)
            {
                return null;
            }
            else
            {
                _userRepository.Insert(user);
            }

            return user;
        }

        public PagedResult<User> GetPaged(int page, int limit)
        {
            return this._userRepository.GetPaged(page, limit);
        }
    }
}