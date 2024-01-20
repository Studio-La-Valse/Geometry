namespace StudioLaValse.Geometry
{
    /// <summary>
    /// The base class for all geometry.
    /// </summary>
    public class BaseGeometry
    {
        /// <summary>
        /// The base value for all geometric operations.
        /// </summary>
        public static double threshold = 0.001;
    }

    /// <summary>
    /// Extensions for geometry.
    /// </summary>
    public static class GeometryExtensions
    {
        /// <summary>
        /// Determines wether two doubles are almost equals, according to the base geometric threshold.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool AlmostEqualTo(this double d, double other)
        {
            return Math.Abs(d - other) < BaseGeometry.threshold;
        }
    }
}
