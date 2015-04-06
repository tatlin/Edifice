using System;
using System.Dynamic;
using EdificeDb;

namespace EdificeCore
{
    public static class ElementFactory
    {
        private static bool isInitialized;

        public static IEdificeCollection Collection { get; private set; }

        /// <summary>
        /// Initialize the ElementFactory.
        /// </summary>
        /// <param name="connection"></param>
        public static void Initialize(IEdificeCollection collection)
        {
            if (isInitialized)
            {
                throw new Exception("The ElementFactory cannot be initialized twice.");
            }

            Collection = collection;

            isInitialized = true;
        }

        /// <summary>
        /// Save the element in the database.
        /// </summary>
        /// <param name="element">The Element to save.</param>
        private static void SaveElement(Element element)
        {
            var el = Collection.FindElementById(element.Id);
            if (el != null)
            {
                Collection.UpdateElement(element);
            }
            else
            {
                Collection.CreateElement(element);
            }
        }

        /// <summary>
        /// Delete the element from the database.
        /// </summary>
        /// <param name="element">The Element to delete.</param>
        private static void DeleteElement(Element element)
        {
            var el = Collection.FindElementById(element.Id);
            if (el != null)
            {
                Collection.DeleteElementById(element.Id);
            }
        }

        public static Grid CreateGrid(GridCreationParameters parameters)
        {
            if (string.IsNullOrEmpty(parameters.Name))
            {
                throw new ArgumentException("You must supply a name.");
            }

            if (parameters.Start == null)
            {
                throw new ArgumentException("You must supply a valid start point.");
            }

            if (parameters.End == null)
            {
                throw new ArgumentException("You must supply a valid end point.");
            }

            if (parameters.Plane == null)
            {
                throw new ArgumentException("You must supply a valid normal.");
            }

            var grid = new Grid(parameters);

            SaveElement(grid);

            return grid;
        }
    }
}
