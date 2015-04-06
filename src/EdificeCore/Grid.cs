using Autodesk.DesignScript.Geometry;

namespace EdificeCore
{
    public class Grid : Datum
    {
        public Point Start { get; set; }
        public Point End { get; set; }

        internal Grid() { }

        internal Grid(GridCreationParameters parameters) : 
            base(parameters.Name, parameters.Plane)
        {
            Name = parameters.Name;
            Start = parameters.Start;
            End = parameters.End;
        }
    }

    public class GridCreationParameters
    {
        public string Name { get; set; }
        public Plane Plane { get; set; }
        public Point Start { get; set; }
        public Point End { get; set; }

        public GridCreationParameters() { }
    }
}
