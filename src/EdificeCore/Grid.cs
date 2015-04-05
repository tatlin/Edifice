using Autodesk.DesignScript.Geometry;

namespace EdificeCore
{
    public class Grid : Datum
    {
        public Point Start { get; set; }
        public Point End { get; set; }

        public Grid(){}

        public Grid(string name, Plane plane, Point start, Point end) : 
            base(name, plane)
        {
            Name = name;
            Start = start;
            End = end;
        }
    }
}
