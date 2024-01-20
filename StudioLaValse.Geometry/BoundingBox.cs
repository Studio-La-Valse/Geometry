namespace StudioLaValse.Geometry
{
    /// <summary>
    /// A class that represents an immutable bounding box.
    /// </summary>
    public class BoundingBox
    {
        /// <summary>
        /// The min point. X- and Y coordinates are always smaller than the X- and Y coordinates of the Max Point.
        /// </summary>
        public XY MinPoint { get; }
        /// <summary>
        /// The max point.
        /// </summary>
        public XY MaxPoint { get; }
        /// <summary>
        /// The center of the bounding box.
        /// </summary>
        public XY Center => (MaxPoint + MinPoint) / 2;
        /// <summary>
        /// The height of the bounding box.
        /// </summary>
        public double Height => MaxPoint.Y - MinPoint.Y;
        /// <summary>
        /// The width of the bounding box.
        /// </summary>
        public double Width => MaxPoint.X - MinPoint.X;

        /// <summary>
        /// The default constructor using doubles.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        public BoundingBox(double x1, double x2, double y1, double y2)
        {
            MinPoint = new XY(Math.Min(x1, x2), Math.Min(y1, y2));
            MaxPoint = new XY(Math.Max(x1, x2), Math.Max(y1, y2));
        }

        /// <summary>
        /// Construct a bounding box from two points.
        /// </summary>
        /// <param name="firstPoint"></param>
        /// <param name="secondPoint"></param>
        public BoundingBox(XY firstPoint, XY secondPoint)
        {
            MinPoint = new XY(Math.Min(firstPoint.X, secondPoint.X), Math.Min(firstPoint.Y, secondPoint.Y));
            MaxPoint = new XY(Math.Max(firstPoint.X, secondPoint.X), Math.Max(firstPoint.Y, secondPoint.Y));
        }

        /// <summary>
        /// Create a bounding box by creating a bounding box around other bounding boxes.
        /// If no boxes are provided, a zero-size bounding box is created.
        /// </summary>
        /// <param name="boxes"></param>
        public BoundingBox(IEnumerable<BoundingBox> boxes)
        {
            if (!boxes.Any())
            {
                MinPoint = new XY(0, 0);
                MaxPoint = new XY(0, 0);

                return;
            }

            var minX = double.MaxValue;
            var minY = double.MaxValue;
            var maxX = double.MinValue;
            var maxY = double.MinValue;

            foreach (var item in boxes)
            {
                minX = Math.Min(item.MinPoint.X, minX);
                minY = Math.Min(item.MinPoint.Y, minY);
                maxX = Math.Max(item.MaxPoint.X, maxX);
                maxY = Math.Max(item.MaxPoint.Y, maxY);
            }

            MinPoint = new XY(minX, minY);
            MaxPoint = new XY(maxX, maxY);
        }
    }

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
                return false;

            if (left.MaxPoint.Y < right.MinPoint.Y)
                return false;

            if (left.MinPoint.X > right.MaxPoint.X)
                return false;

            if (left.MinPoint.Y > right.MaxPoint.Y)
                return false;

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
            if (boundingBox.MaxPoint.X < point.X)
                return false;

            if (boundingBox.MaxPoint.Y < point.Y)
                return false;

            if (boundingBox.MinPoint.X > point.X)
                return false;

            if (boundingBox.MinPoint.Y > point.Y)
                return false;

            return true;
        }
    }
}
