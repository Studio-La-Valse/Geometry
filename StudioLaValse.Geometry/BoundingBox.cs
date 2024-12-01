namespace StudioLaValse.Geometry;

/// <summary>
/// A class that represents an immutable bounding box.
/// </summary>
public readonly struct BoundingBox
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

    /// <summary>
    /// Returns the closest point within the bounding box to the specified point.
    /// </summary>
    /// <param name="other">The point to find the closest point to.</param>
    /// <returns>The closest point within the bounding box.</returns>
    public XY ClosestPointShape(XY other)
    {
        var closestX = Math.Max(MinPoint.X, Math.Min(other.X, MaxPoint.X));
        var closestY = Math.Max(MinPoint.Y, Math.Min(other.Y, MaxPoint.Y));
        return new XY(closestX, closestY);
    }

    /// <summary>
    /// Returns the closest point on the edge of the bounding box to the specified point.
    /// </summary>
    /// <param name="other">The point to find the closest point to.</param>
    /// <returns>The closest point on the edge of the bounding box.</returns>
    public XY ClosestPointEdge(XY other)
    {
        // Find the closest point within the bounding box
        var closestX = Math.Max(MinPoint.X, Math.Min(other.X, MaxPoint.X));
        var closestY = Math.Max(MinPoint.Y, Math.Min(other.Y, MaxPoint.Y));
        var closestPoint = new XY(closestX, closestY);

        // Check if the closest point is already on the edge
        if (closestX == MinPoint.X || closestX == MaxPoint.X ||
            closestY == MinPoint.Y || closestY == MaxPoint.Y)
        {
            return closestPoint;
        }

        // Determine the closest distance to an edge
        var deltaXMin = Math.Abs(other.X - MinPoint.X);
        var deltaXMax = Math.Abs(other.X - MaxPoint.X);
        var deltaYMin = Math.Abs(other.Y - MinPoint.Y);
        var deltaYMax = Math.Abs(other.Y - MaxPoint.Y);

        var minDeltaX = Math.Min(deltaXMin, deltaXMax);
        var minDeltaY = Math.Min(deltaYMin, deltaYMax);

        // Check which edge is closest and return the correct edge point
        // Added logic for consistent behavior when distances are equal
        if (minDeltaX < minDeltaY || (minDeltaX == minDeltaY && deltaXMin < deltaXMax))
        {
            return new XY((deltaXMin < deltaXMax) ? MinPoint.X : MaxPoint.X, other.Y);
        }
        else
        {
            return new XY(other.X, (deltaYMin < deltaYMax) ? MinPoint.Y : MaxPoint.Y);
        }
    }
}
