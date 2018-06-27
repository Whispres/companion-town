using MongoDB.Driver;

namespace Api.Repositories
{
    public abstract class BaseRepository<T>
    {
        private readonly IMongoDatabase _mongoDatabase;

        public readonly string _collectionName;

        private IMongoCollection<T> Collection;

        public BaseRepository(IMongoDatabase mongoDatabase, string collectionName)
        {
            this._mongoDatabase = mongoDatabase;
            this._collectionName = collectionName;
        }

        protected IMongoCollection<T> GetCollection()
        {
            if (this.Collection == null)
            {
                this.Collection = _mongoDatabase.GetCollection<T>(this._collectionName);
            }

            return this.Collection;
        }
    }
}