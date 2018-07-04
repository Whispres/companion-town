using System;
using Api.Models;
using Api.Options;
using Microsoft.Extensions.Options;
using Serilog;

namespace Api.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(IOptions<DatabaseOptions> databaseOptions) :
            base(databaseOptions.Value.CompanionTownConnectionString, databaseOptions.Value.UsersCollection)
        {
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
    }
}