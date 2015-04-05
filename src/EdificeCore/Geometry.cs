namespace EdificeCore
{
    public class Vector3
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Vector3()
        {
            X = 0.0;
            Y = 0.0;
            Z = 0.0;
        }

        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
