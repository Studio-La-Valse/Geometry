namespace StudioLaValse.Geometry
{
    /// <summary>
    /// Represents an immutable circle.
    /// </summary>
    public class Circle
    {
        /// <summary>
        /// The center of the circle.
        /// </summary>
        public XY Center { get; }
        /// <summary>
        /// The radius of the circle.
        /// </summary>
        public double Radius { get; }

        /// <summary>
        /// Constructs a circle from a center and a radius.
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        public Circle(XY center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        /// <summary>
        /// Constructs a circle from x- and y coordinates and a radius.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="radius"></param>
        public Circle(double x, double y, double radius)
        {
            Center = new XY(x, y);
            Radius = radius;
        }
    }
}
