namespace StudioLaValse.Geometry
{
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
        /// <param name="threshold"></param>
        /// <returns></returns>
        public static bool AlmostEqualTo(this double d, double other, double? threshold = null)
        {
            threshold ??= BaseGeometry.threshold;
            return Math.Abs(d - other) < threshold;
        }
    }
}
