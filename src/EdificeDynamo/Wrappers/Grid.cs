using Autodesk.DesignScript.Geometry;
using Autodesk.DesignScript.Interfaces;
using EdificeCore;

namespace EdificeDynamo.Wrappers
{
    public class Grid : Element, IGraphicItem
    {
        private Grid(GridCreationParameters parameters)
        {
            var grid = EdificeObjectManager.FindElement() as EdificeCore.Grid;

            if (grid == null)
            {
                grid = ElementFactory.CreateGrid(parameters);
            }
            else
            {
                grid.Name = parameters.Name;
                grid.Plane = parameters.Plane;
                grid.Start = parameters.Start;
                grid.End = parameters.End;
            }

            InternalElement = grid;

            EdificeObjectManager.RegisterTraceableObjectForId(new TraceableId(grid.Id), grid);  
        }

        public static Grid ByPlane(string name, Plane plane, Point start, Point end)
        {
            var parameters = new GridCreationParameters()
            {
                Name = name,
                Plane = plane,
                Start = start,
                End = end
            };

            return new Grid(parameters);
        }


        public void Tessellate(IRenderPackage package, TessellationParameters parameters)
        {
            var el = InternalElement as EdificeCore.Grid;
            if (el == null) return;

            // draw the grid
            package.AddTriangleVertex(el.Start.X, el.Start.Y, el.Start.Z);
            package.AddTriangleVertex(el.End.X, el.End.Y, el.End.Z);
            package.AddTriangleVertex(el.Start.X, el.Start.Y, el.End.Z);
            //package.

            
            package.AddLineStripVertex(el.Start.X, el.Start.Y, el.Start.Z);
            package.AddLineStripVertex(el.End.X, el.End.Y, el.End.Z);
            package.AddLineStripVertexCount(2);
            package.AddLineStripVertexColor(100, 100, 100, 255);
            package.AddLineStripVertexColor(100, 100, 100, 255);
        }

        public override string ToString()
        {
            var g = InternalElement as EdificeCore.Grid;
            if (g == null) return base.ToString();

            return string.Format("Name:{2}, Start:{0}, End:{1}", g.Start, g.End, g.Name);
        }
    }
}
