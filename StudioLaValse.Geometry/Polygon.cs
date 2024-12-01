using System.Collections.ObjectModel;

namespace StudioLaValse.Geometry
{
    /// <summary>
    /// Represents an immutable polygon, which is a closed shape made up of connected points.
    /// </summary>
    public class Polygon
    {
        /// <summary>
        /// Gets the points that make up the polygon.
        /// </summary>
        public IReadOnlyList<XY> Points { get; }

        /// <summary>
        /// Constructs a polygon from an enumerable of points.
        /// </summary>
        /// <param name="points">The points to form the polygon.</param>
        /// <exception cref="InvalidOperationException">Thrown when less than three points are given.</exception>
        public Polygon(IEnumerable<XY> points)
        {
            var pointList = new List<XY>(points);
            if (pointList.Count < 3)
            {
                throw new InvalidOperationException("A polygon must have at least three points.");
            }
            Points = new ReadOnlyCollection<XY>(pointList);
        }

        /// <summary>
        /// Returns the line segments that make up the polygon.
        /// </summary>
        /// <returns>An enumerable of <see cref="Line"/> segments.</returns>
        public IEnumerable<Line> GetLineSegments()
        {
            for (var i = 0; i < Points.Count; i++)
            {
                var start = Points[i];
                var end = Points[(i + 1) % Points.Count];
                yield return new Line(start, end);
            }
        }

        /// <summary>
        /// Finds the closest point on the edge of the polygon to the specified point.
        /// </summary>
        /// <param name="other">The point to find the closest point to.</param>
        /// <returns>The closest point on the edge of the polygon.</returns>
        public XY ClosestPointEdge(XY other)
        {
            XY? closestPoint = null;
            var closestDistance = double.MaxValue;

            foreach (var line in GetLineSegments())
            {
                var candidate = line.ClosestPoint(other);
                var distance = candidate.DistanceTo(other);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPoint = candidate;
                }
            }

            if (closestPoint is null)
            {
                throw new InvalidOperationException("The polygon does not contain any points.");
            }

            return closestPoint.Value;
        }

        /// <summary>
        /// Finds the closest point inside the polygon to the specified point.
        /// </summary>
        /// <param name="other">The point to find the closest point to.</param>
        /// <returns>The closest point inside the polygon.</returns>
        public XY ClosestPointShape(XY other)
        {
            var closestPoint = ClosestPointEdge(other);

            if (IsPointInPolygon(other))
            {
                return other;
            }

            return closestPoint;
        }

        /// <summary>
        /// Determines if a point is inside the polygon using the ray-casting algorithm.
        /// </summary>
        /// <param name="point">The point to check.</param>
        /// <returns>True if the point is inside the polygon, otherwise false.</returns>
        public bool IsPointInPolygon(XY point)
        {
            var result = false;
            var j = Points.Count - 1;

            for (var i = 0; i < Points.Count; i++)
            {
                if (Points[i].Y < point.Y && Points[j].Y >= point.Y || Points[j].Y < point.Y && Points[i].Y >= point.Y)
                {
                    if (Points[i].X + (point.Y - Points[i].Y) / (Points[j].Y - Points[i].Y) * (Points[j].X - Points[i].X) < point.X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }

            return result;
        }
    }

}
