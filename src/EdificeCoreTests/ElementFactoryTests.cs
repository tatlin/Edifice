using EdificeCore;
using EdificeMongoDb;
using NUnit.Framework;

namespace EdificeCoreTests
{
    [TestFixture]
    public class ElementFactoryTests
    {
        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            var connection = new EdificeMongoDbConnection(EdificeMongoDbConnection.TEST_DB_NAME);
            var db = connection.FindDatabaseByName(EdificeMongoDbConnection.TEST_DB_NAME);
            var coll = db.FindElementCollectionByName(EdificeMongoDbConnection.TEST_COLLECTION_NAME);

            ElementFactory.Initialize(coll);
        }
    }
}
