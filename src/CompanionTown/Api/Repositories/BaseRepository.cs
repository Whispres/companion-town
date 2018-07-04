using LiteDB;

namespace Api.Repositories
{
    public abstract class BaseRepository<T>
    {
        public BaseRepository(string connectionString, string collectionName)
        {
            var repo = new LiteRepository(connectionString);
            this.Database = repo.Database.GetCollection<T>(collectionName);
        }

        public LiteCollection<T> Database { get; }
    }
}