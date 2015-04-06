using System;
using EdificeCore;
using EdificeDb;
using EdificeMongoDb;
using NUnit.Framework;

namespace EdificeMongoDbTests
{
    public class TestElement : Element
    {
        public TestElement(string name, Guid id) : base(id)
        {
            Name = name;
        }    
    }

    [TestFixture]
    public class EdificeMongoDbTests
    {
        private Guid TEST_GUID_A = Guid.Parse("23a53d2d-ae41-4e7f-b436-9d25840f0847");
        private Guid TEST_GUID_B = Guid.Parse("ff382a5d-8fe4-4cbb-abc8-39d10d7b4af0");
        private Guid TEST_GUID_C = Guid.Parse("b98e030a-82c1-41aa-8888-7231b6dc80e0");

        [SetUp]
        public void Setup()
        {
            var connection = new EdificeMongoDbConnection(EdificeMongoDbConnection.TEST_DB_NAME);

            var db = connection.FindDatabaseByName(EdificeMongoDbConnection.TEST_DB_NAME);

            // Add three elements
            var e1 = new TestElement("ElementA",TEST_GUID_A);
            var e2 = new TestElement("ElementB",TEST_GUID_B);
            var e3 = new TestElement("ElementC",TEST_GUID_C);

            var collection = db.FindElementCollectionByName(EdificeMongoDbConnection.TEST_COLLECTION_NAME);

            collection.CreateElement(e1);
            collection.CreateElement(e2);
            collection.CreateElement(e3);
        }

        [TearDown]
        public void Teardown()
        {
            var coll = GetTestEdificeCollection();
            coll.RemoveAllElements();
        }

        [Test]
        public void CanConnectToDb()
        {
            var connection = new EdificeMongoDbConnection(EdificeMongoDbConnection.TEST_DB_NAME);
            Assert.NotNull(connection);
        }

        [Test]
        public void ReturnsNullWhenAttemptingToGeDbThatDoesNotExist()
        {
            var connection = new EdificeMongoDbConnection(EdificeMongoDbConnection.TEST_DB_NAME);
            Assert.NotNull(connection);
            var db = connection.FindDatabaseByName("blah");
            Assert.Null(db);
        }

        [Test]
        public void ReturnsCollectionIfCollectionExists()
        {
            var coll = GetTestEdificeCollection();
            Assert.NotNull(coll);
        }

        [Test]
        public void ReturnsNullIfCollectionDoesNotExist()
        {
            var db = GetTestDatabase();
            Assert.NotNull(db);
            var coll = db.FindElementCollectionByName("blah");
            Assert.Null(coll);
        }

        [Test]
        public void ThrowsExceptionWhenAttemptingToCreateElementWhichAlreadyExists()
        {
            var e1 = new TestElement("ElementA", TEST_GUID_A);
            var coll = GetTestEdificeCollection();
            Assert.Throws<Exception>(()=>coll.CreateElement(e1));
        }

        [Test]
        public void CreatesAnElementIfOneDoesNotExist()
        {
            var e1 = new TestElement("ElementD", Guid.NewGuid());
            var coll = GetTestEdificeCollection();
            Assert.DoesNotThrow(() => coll.CreateElement(e1));
        }

        [Test]
        public void DeletesElementIfElementExists()
        {
            var coll = GetTestEdificeCollection();
            Assert.DoesNotThrow(()=>coll.DeleteElementById(TEST_GUID_A));
        }

        [Test]
        public void ThrowsExceptionWhenAttemptingToDeleteElementThatDoesNotExist()
        {
            var coll = GetTestEdificeCollection();
            Assert.Throws<Exception>(() => coll.DeleteElementById(Guid.NewGuid()));
        }

        [Test]
        public void ElementCanBeUpdated()
        {
            var coll = GetTestEdificeCollection();
            var el = coll.FindElementById(TEST_GUID_A);

            el.Name = "booyah";
            coll.UpdateElement(el);

            var el2 = coll.FindElementById(TEST_GUID_A);
            Assert.AreEqual(el2.Name, "booyah");
        }

        [Test]
        public void ThrowsExceptionWhenAttemptingToUpdateElementThatDoesNotExist()
        {
            var coll = GetTestEdificeCollection();
            var grid = ElementFactory.CreateGrid(new GridCreationParameters());
            Assert.Throws<Exception>(() => coll.UpdateElement(grid));
        }

        [Test]
        public void ReturnsElementIfElementExists()
        {
            var coll = GetTestEdificeCollection();
            var el = coll.FindElementById(TEST_GUID_A);
            Assert.NotNull(el);
        }

        [Test]
        public void ReturnsNullIfElementDoesNotExist()
        {
            var coll = GetTestEdificeCollection();
            var el = coll.FindElementById(Guid.NewGuid());
            Assert.Null(el);
        }

        private static IEdificeCollection GetTestEdificeCollection()
        {
            var db = GetTestDatabase();
            var coll = db.FindElementCollectionByName(EdificeMongoDbConnection.TEST_COLLECTION_NAME);
            return coll;
        }

        private static IEdificeDatabase GetTestDatabase()
        {
            var connection = new EdificeMongoDbConnection(EdificeMongoDbConnection.TEST_DB_NAME);
            var db = connection.FindDatabaseByName(EdificeMongoDbConnection.TEST_DB_NAME);
            return db;
        }
    }
}
