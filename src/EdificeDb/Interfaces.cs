using System;

namespace EdificeDb
{
    public interface IEdificeElement
    {
        string Name { get; set; }
        Guid Id { get; }
        DateTime CreatedAt { get; }
        DateTime UpdatedAt { get; set; }
    }

    public interface IEdificeConnection
    {
        /// <summary>
        /// Get a database by name at the connection.
        /// </summary>
        /// <param name="name">The name of the database to return.</param>
        /// <returns>An IEdificeDatabase object or null.</returns>
        IEdificeDatabase FindDatabaseByName(string name);
    }

    public interface IEdificeDatabase
    {
        /// <summary>
        /// Get a collection given a name.
        /// </summary>
        /// <param name="collectionName">The name of the collection.</param>
        /// <returns>A collection if one is found with the supplied name, or null.</returns>
        IEdificeCollection FindElementCollectionByName(string collectionName);
    }

    public interface IEdificeCollection
    {
        /// <summary>
        /// Creates a record for an element in the collection.
        /// </summary>
        /// <param name="element">The Element to create or update.</param>
        void CreateElement(IEdificeElement element);

        /// <summary>
        /// Updates a record for an element in the collection
        /// </summary>
        /// <param name="element">The element to update in the collection.</param>
        void UpdateElement(IEdificeElement element);

        /// <summary>
        /// Finds an element in the collection given an id.
        /// </summary>
        /// <param name="id">The id of the element.</param>
        /// <returns>An Element or null if the element cannot by found.</returns>
        IEdificeElement FindElementById(Guid id);

        /// <summary>
        /// Deletes an element in the collection given an id.
        /// </summary>
        /// <param name="id">The id of the element.</param>
        void DeleteElementById(Guid id);

        /// <summary>
        /// Removes all elements from the collection.
        /// </summary>
        void RemoveAllElements();
    }
}
