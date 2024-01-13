namespace StudioLaValse.Geometry
{
    public class Circle
    {
        public XY Center { get; }
        public double Radius { get; }

        public Circle(XY center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        public Circle(double x, double y, double radius)
        {
            Center = new XY(x, y);
            Radius = radius;
        }
    }
}
