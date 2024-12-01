using System.Collections.ObjectModel;

namespace StudioLaValse.Geometry
{
    /// <summary>
    /// Represents an immutable polyline, which is a series of connected points.
    /// </summary>
    public class Polyline
    {
        /// <summary>
        /// Gets the points that make up the polyline.
        /// </summary>
        public IReadOnlyList<XY> Points { get; }

        /// <summary>
        /// Constructs a polyline from an enumerable of points.
        /// </summary>
        /// <param name="points">The points to form the polyline.</param>
        /// <exception cref="InvalidOperationException">Thrown when no points are given.</exception>
        public Polyline(IEnumerable<XY> points)
        {
            var pointList = new List<XY>(points);
            if (pointList.Count == 0)
            {
                throw new InvalidOperationException("No points were given.");
            }
            Points = new ReadOnlyCollection<XY>(pointList);
        }

        /// <summary>
        /// Calculates the total length of the polyline.
        /// </summary>
        /// <returns>The total length.</returns>
        public double TotalLength()
        {
            double totalLength = 0;
            XY? previousPoint = null;

            foreach (var point in Points)
            {
                if (previousPoint != null)
                {
                    totalLength += previousPoint.Value.DistanceTo(point);
                }

                previousPoint = point;
            }

            return totalLength;
        }

        /// <summary>
        /// Finds the closest point on the polyline to the specified point.
        /// </summary>
        /// <param name="other">The point to find the closest point to.</param>
        /// <returns>The closest point on the polyline.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the polyline is empty.</exception>
        public XY ClosestPoint(XY other)
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
                throw new InvalidOperationException("The polyline does not contain any points.");
            }

            return closestPoint.Value;
        }

        /// <summary>
        /// Returns the line segments that make up the polyline.
        /// </summary>
        /// <returns>An enumerable of <see cref="Line"/> segments.</returns>
        public IEnumerable<Line> GetLineSegments()
        {
            XY? previousPoint = null;

            foreach (var point in Points)
            {
                if (previousPoint != null)
                {
                    yield return new Line(previousPoint.Value, point);
                }

                previousPoint = point;
            }
        }
    }

}
