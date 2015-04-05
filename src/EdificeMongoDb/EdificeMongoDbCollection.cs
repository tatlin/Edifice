using System;
using EdificeCore;
using EdificeDb;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace EdificeMongoDb
{
    class EdificeMongoDbCollection : IEdificeCollection
    {
        private MongoCollection collection;

        internal EdificeMongoDbCollection(MongoDatabase db, string collectionName)
        {
            collection = db.GetCollection<Element>(collectionName);
        }

        public void CreateElement(IEdificeElement element)
        {
            if (FindElementById(element.Id) != null)
            {
                throw new Exception("Element already exists.");
            }

            collection.Insert(element);
        }

        public void UpdateElement(IEdificeElement element)
        {
            if (FindElementById(element.Id) == null)
            {
                throw new Exception("Element cannot be updated.");
            }
            element.UpdatedAt = DateTime.UtcNow;
            collection.Save(element);
        }

        public IEdificeElement FindElementById(Guid id)
        {
            var query = Query<Element>.EQ(e => e.Id, id);
            return collection.FindOneAs<Element>(query);
        }

        public void DeleteElementById(Guid id)
        {
            var element = FindElementById(id);
            if (element == null)
            {
                throw new Exception("The specified element could not be found for deletion.");
            }

            var query = Query<Element>.EQ(e => e.Id, id);
            collection.Remove(query);
        }

        public void RemoveAllElements()
        {
            collection.RemoveAll();
        }
    }
}
