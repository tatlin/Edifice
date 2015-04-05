using Autodesk.DesignScript.Geometry;

namespace EdificeCore
{
    public class Datum : Element
    {
        public Plane Plane { get; set; }

        internal Datum(){}

        internal Datum(string name, Plane plane)
        {
            Name = name;
            Plane = plane;
        }
    }
}
