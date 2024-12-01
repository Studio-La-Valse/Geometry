namespace StudioLaValse.Geometry;

/// <summary>
/// Represents an immutable circle.
/// </summary>
public readonly struct Circle
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

    /// <summary>
    /// Returns the closest point inside the circle to the given point.
    /// </summary>
    /// <param name="other">The point to find the closest point to.</param>
    /// <returns>The closest point inside the circle.</returns>
    public XY ClosestPointShape(XY other)
    {
        var dx = other.X - Center.X;
        var dy = other.Y - Center.Y;
        var distance = Math.Sqrt(dx * dx + dy * dy);

        if (distance <= Radius)
        {
            // The point is inside the circle
            return other;
        }

        // Scale down to the edge of the circle
        var scale = Radius / distance;
        return new XY(Center.X + dx * scale, Center.Y + dy * scale);
    }

    /// <summary>
    /// Returns the closest point on the edge of the circle to the given point.
    /// </summary>
    /// <param name="other">The point to find the closest point to.</param>
    /// <returns>The closest point on the edge of the circle.</returns>
    public XY ClosestPointEdge(XY other)
    {
        var dx = other.X - Center.X;
        var dy = other.Y - Center.Y;
        var distance = Math.Sqrt(dx * dx + dy * dy);

        if (distance.AlmostEqualTo(0))
        {
            // If the other point is exactly at the center, any point on the edge is equally close.
            return new XY(Center.X + Radius, Center.Y);
        }

        // Scale to the edge of the circle
        var scale = Radius / distance;
        return new XY(Center.X + dx * scale, Center.Y + dy * scale);
    }
}


