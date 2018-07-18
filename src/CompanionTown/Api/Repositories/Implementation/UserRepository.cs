using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Api.Options;
using Microsoft.Extensions.Options;
using Serilog;

namespace Api.Repositories.Implementation
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IOptions<DatabaseOptions> databaseOptions) :
            base(databaseOptions.Value.CompanionTownConnectionString, databaseOptions.Value.UsersCollection)
        {
        }

        public Task<User> Get(string name)
        {
            try
            {
                return Task.Run(() => this.Database.Find(_ => _.Name == name).FirstOrDefault());
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"On {nameof(Get)}");

                return null;
            }
        }

        public PagedResult<User> GetPaged(int skip, int limit)
        {
            var result = new PagedResult<User>()
            {
                Page = 0, // todo
                Take = limit
            };

            try
            {
                result.Results = this.Database.Find(_ => _.Id != Guid.Empty, skip, limit).ToList();

                if (result.Total <= 0)
                {
                    result.Total = this.Count();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"On {nameof(GetPaged)}");

                return result;
            }

            return result;
        }

        public bool Insert(User user)
        {
            try
            {
                this.Database.Insert(user);

                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"On {nameof(Insert)}");

                return false;
            }
        }

        public int Count()
        {
            try
            {
                return this.Database.Count();
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"On {nameof(Count)}");

                return 0;
            }
        }
    }
}