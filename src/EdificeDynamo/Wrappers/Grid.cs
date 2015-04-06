using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Interfaces;
using EdificeCore;

namespace EdificeDynamo.Wrappers
{
    public class Grid : Element, IGraphicItem
    {
        {
            var grid = EdificeObjectManager.FindElement() as EdificeCore.Grid;

            if (grid == null)
            {
                var parameters = new GridCreationParameters()
                {
                    Name = name,
                    Plane = plane,
                    Start = start,
                    End = end
                };

                grid = ElementFactory.CreateGrid(parameters);
            }
            else
            {
                grid.Name = name;
                grid.Plane = plane;
                grid.Start = start;
                grid.End = end;
            }

            InternalElement = grid;

            EdificeObjectManager.RegisterTraceableObjectForId(new TraceableId(grid.Id), grid);  
        }

        public void Tessellate(IRenderPackage package, double tol = -1, int maxGridLines = 512)
        {
            var el = InternalElement as EdificeCore.Grid;
            if (el == null) return;

            // draw the grid
            package.PushLineStripVertex(el.Start.X, el.Start.Y, el.Start.Z);
            package.PushLineStripVertex(el.End.X, el.End.Y, el.End.Z);
            package.PushLineStripVertexCount(2);
            package.PushLineStripVertexColor(100, 100, 100, 255);
            package.PushLineStripVertexColor(100, 100, 100, 255);
        }

        public override string ToString()
        {
            var g = InternalElement as EdificeCore.Grid;
            if (g == null) return base.ToString();

            return string.Format("Name:{2}, Start:{0}, End:{1}", g.Start, g.End, g.Name);
        }
    }
}
