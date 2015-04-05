using System;
using EdificeMongoDb;

namespace EdificeDynamo.Wrappers
{
    public abstract class Element : IDisposable
    {
        internal EdificeCore.Element InternalElement { get; set; }

        /// <summary>
        /// Update or save the element in the database.
        /// </summary>
        public void Save()
        {
            var connection = new EdificeMongoDbConnection();

            var db = connection.FindDatabaseByName(EdificeMongoDbConnection.DEFAULT_DB_NAME);
            var coll = db.FindElementCollectionByName(EdificeMongoDbConnection.DEFAULT_ELEMENT_COLLECTION_NAME);

            var el = coll.FindElementById(InternalElement.Id);
            if (el != null)
            {
                coll.UpdateElement(InternalElement);
            }
            else
            {
                coll.CreateElement(InternalElement);
            }
        }

        private void Delete()
        {
            var connection = new EdificeMongoDbConnection();
            var db = connection.FindDatabaseByName("edifice");
            var coll = db.FindElementCollectionByName("elements");
            var el = coll.FindElementById(InternalElement.Id);
            if (el != null)
            {
                coll.DeleteElementById(InternalElement.Id);
            }
        }

        public void Dispose()
        {
            Delete();
        }
    }
}
