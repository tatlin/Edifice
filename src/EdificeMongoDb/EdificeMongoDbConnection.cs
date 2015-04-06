using System;
using EdificeDb;
using MongoDB.Driver;

namespace EdificeMongoDb
{
    public class EdificeMongoDbConnection : IEdificeConnection
    {
        public const string DEFAULT_DB_NAME = "edifice";
        public const string DEFAULT_ELEMENT_COLLECTION_NAME = "elements";
        public const string TEST_DB_NAME = "edifice_test";
        public const string TEST_COLLECTION_NAME = "elements";

        private MongoServer server;
        private const string CONNECTION_STRING = "mongodb://localhost";
        private string elementCollectionName;
        private string dbName;

        public EdificeMongoDbConnection(string dbName = DEFAULT_DB_NAME, string elementCollectionName = DEFAULT_ELEMENT_COLLECTION_NAME)
        {
            this.dbName = dbName;
            this.elementCollectionName = elementCollectionName;

            var client = new MongoClient(CONNECTION_STRING);
            server = client.GetServer();
            if (server == null)
            {
                throw new Exception("A connection to the server could not be established.");
            }

            Initialize();
        }

        /// <summary>
        /// Create necessary databases and collections.
        /// </summary>
        private void Initialize()
        {
            var db = server.GetDatabase(dbName);
            if (!db.CollectionExists(elementCollectionName))
            {
                db.CreateCollection(elementCollectionName);
            }
        }

        public IEdificeDatabase FindDatabaseByName(string dbName)
        {
            return !server.DatabaseExists(dbName) ? 
                null : 
                new EdificeMongoDb(server, dbName);
        }
    }
}
