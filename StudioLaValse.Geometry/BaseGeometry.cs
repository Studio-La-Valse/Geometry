namespace StudioLaValse.Geometry
{
    public class BaseGeometry
    {
        public static double threshold = 0.001;
    }

    public static class GeometryExtensions
    {
        public static bool AlmostEqualTo(this double d, double other)
        {
            return Math.Abs(d - other) < BaseGeometry.threshold;
        }
    }
}
