using System;
using System.Threading.Tasks;
using Api.Models;
using MongoDB.Driver;

namespace Api.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        // hack : magic string to be removed to config !?!?!
        public UserRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase, "Users")
        {
        }

        public async Task<bool> InsertAsync(User user)
        {
            try
            {
                await this.GetCollection().InsertOneAsync(user).ConfigureAwait(false);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}