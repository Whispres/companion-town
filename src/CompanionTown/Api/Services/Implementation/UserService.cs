﻿using System.Threading.Tasks;
using Api.Exceptions;
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

        public async Task<User> CreateUserAsync(User user)
        {
            var existentUser = await this._userRepository.GetAsync(user.Name);

            if (existentUser != null)
            {
                throw new NotModifiedException("Already exists");
            }
            else
            {
                await _userRepository.InsertAsync(user);
            }

            return user;
        }

        public PagedResult<User> GetPaged(int page, int limit)
        {
            return this._userRepository.GetPaged(page, limit);
        }

        public async Task<User> GetAsync(string id)
        {
            return await this._userRepository.GetAsync(id);
        }
    }
}