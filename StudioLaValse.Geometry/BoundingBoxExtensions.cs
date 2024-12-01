namespace StudioLaValse.Geometry
{
    /// <summary>
    /// Extensions for the bounding box class.
    /// </summary>
    public static class BoundingBoxExtensions
    {
        /// <summary>
        /// Expand a bounding box horizontally and vertically in both directions for a set offset.
        /// </summary>
        /// <param name="boundingBox"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static BoundingBox Expand(this BoundingBox boundingBox, double offset)
        {
            return new BoundingBox(boundingBox.MinPoint.X - offset, boundingBox.MaxPoint.X + offset, boundingBox.MinPoint.Y - offset, boundingBox.MaxPoint.Y + offset);
        }

        /// <summary>
        /// Determines wether two bounding boxes occopy at least some space. 
        /// Returns false when the second box lies completely oustside of the first.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Overlaps(this BoundingBox left, BoundingBox right)
        {
            if (left.MaxPoint.X < right.MinPoint.X)
            {
                return false;
            }

            if (left.MaxPoint.Y < right.MinPoint.Y)
            {
                return false;
            }

            if (left.MinPoint.X > right.MaxPoint.X)
            {
                return false;
            }

            if (left.MinPoint.Y > right.MaxPoint.Y)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Returns true when the first bounding box lies completely within the other.
        /// </summary>
        /// <param name="boundingBox"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool ContainedBy(this BoundingBox boundingBox, BoundingBox other)
        {
            return other.Contains(boundingBox.MinPoint) && other.Contains(boundingBox.MaxPoint);
        }

        /// <summary>
        /// Returns true when the other bounding box lies completely within the first.
        /// </summary>
        /// <param name="boundingBox"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool Contains(this BoundingBox boundingBox, BoundingBox other)
        {
            return boundingBox.Contains(other.MinPoint) && boundingBox.Contains(other.MaxPoint);
        }

        /// <summary>
        /// Returns true if the point lies within the bounding box. Returns false when the point lies on the edge, or outside of the bounding box.
        /// </summary>
        /// <param name="boundingBox"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static bool Contains(this BoundingBox boundingBox, XY point)
        {
            if (boundingBox.MaxPoint.X <= point.X)
            {
                return false;
            }

            if (boundingBox.MaxPoint.Y <= point.Y)
            {
                return false;
            }

            if (boundingBox.MinPoint.X >= point.X)
            {
                return false;
            }

            if (boundingBox.MinPoint.Y >= point.Y)
            {
                return false;
            }

            return true;
        }
    }
}
