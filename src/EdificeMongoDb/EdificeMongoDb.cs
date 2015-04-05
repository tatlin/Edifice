using EdificeDb;
using MongoDB.Driver;

namespace EdificeMongoDb
{
    public class EdificeMongoDb : IEdificeDatabase
    {
        private MongoDatabase db;

        internal EdificeMongoDb(MongoServer server, string dbName)
        {
            db = server.GetDatabase(dbName);
        }

        public IEdificeCollection FindElementCollectionByName(string collectionName)
        {
            return !db.CollectionExists(collectionName)
                ? null
                : new EdificeMongoDbCollection(db, collectionName);
        }
    }
}
