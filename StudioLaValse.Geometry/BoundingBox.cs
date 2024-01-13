namespace StudioLaValse.Geometry
{
    public class BoundingBox
    {
        public XY MinPoint { get; }
        public XY MaxPoint { get; }
        public XY Center => (MaxPoint + MinPoint) / 2;
        public double Height => MaxPoint.Y - MinPoint.Y;
        public double Width => MaxPoint.X - MinPoint.X;

        public BoundingBox(double x1, double x2, double y1, double y2)
        {
            MinPoint = new XY(Math.Min(x1, x2), Math.Min(y1, y2));
            MaxPoint = new XY(Math.Max(x1, x2), Math.Max(y1, y2));
        }

        public BoundingBox(XY firstPoint, XY secondPoint)
        {
            MinPoint = new XY(Math.Min(firstPoint.X, secondPoint.X), Math.Min(firstPoint.Y, secondPoint.Y));
            MaxPoint = new XY(Math.Max(firstPoint.X, secondPoint.X), Math.Max(firstPoint.Y, secondPoint.Y));
        }

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

    public static class BoundingBoxExtensions
    {
        public static BoundingBox Expand(this BoundingBox boundingBox, double offset)
        {
            return new BoundingBox(boundingBox.MinPoint.X - offset, boundingBox.MaxPoint.X + offset, boundingBox.MinPoint.Y - offset, boundingBox.MaxPoint.Y + offset);
        }


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

        public static bool ContainedBy(this BoundingBox boundingBox, BoundingBox other)
        {
            return other.Contains(boundingBox.MinPoint) && other.Contains(boundingBox.MaxPoint);
        }

        public static bool Contains(this BoundingBox boundingBox, BoundingBox other)
        {
            return boundingBox.Contains(other.MinPoint) && boundingBox.Contains(other.MaxPoint);
        }

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
