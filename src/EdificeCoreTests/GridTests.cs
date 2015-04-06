using EdificeCore;
using NUnit.Framework;

namespace EdificeCoreTests
{
    [TestFixture]
    public class GridTests : ElementFactoryTests
    {
        [Test]
        public void CanCreateAGrid()
        {
            var grid = ElementFactory.CreateGrid(new GridCreationParameters()
            {
                Name = "A10",
            });
            var grid1 = ElementFactory.Collection.FindElementById(grid.Id);
            Assert.NotNull(grid1);
        }
    }
}
