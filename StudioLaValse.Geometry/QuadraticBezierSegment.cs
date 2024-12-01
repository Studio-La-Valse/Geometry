using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudioLaValse.Geometry;
using System;
using System.Collections.Generic;


/// <summary>
/// Represents a quadratic Bezier curve.
/// </summary>
public readonly struct QuadraticBezierSegment
{
    /// <summary>
    /// Gets the first control point.
    /// </summary>
    public XY FirstControlPoint { get; }

    /// <summary>
    /// Gets the second control point.
    /// </summary>
    public XY SecondControlPoint { get; }

    /// <summary>
    /// Gets the third control point.
    /// </summary>
    public XY ThirdControlPoint { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="QuadraticBezierSegment"/> class with specified control points.
    /// </summary>
    public QuadraticBezierSegment(XY first, XY second, XY third)
    {
        FirstControlPoint = first;
        SecondControlPoint = second;
        ThirdControlPoint = third;
    }

    /// <summary>
    /// Calculates a point on the quadratic Bezier curve at a given t parameter.
    /// </summary>
    /// <param name="t">The t parameter, ranging from 0 to 1.</param>
    /// <returns>The calculated point on the Bezier curve.</returns>
    public XY GetBezierPoint(double t)
    {
        var x = Math.Pow((1 - t), 2) * FirstControlPoint.X + 2 * (1 - t) * t * SecondControlPoint.X + Math.Pow(t, 2) * ThirdControlPoint.X;
        var y = Math.Pow((1 - t), 2) * FirstControlPoint.Y + 2 * (1 - t) * t * SecondControlPoint.Y + Math.Pow(t, 2) * ThirdControlPoint.Y;
        return new XY(x, y);
    }

    /// <summary>
    /// Converts the Bezier curve to a polyline approximation.
    /// </summary>
    /// <param name="segments">The number of segments to approximate the curve.</param>
    /// <returns>A polyline approximation of the Bezier curve.</returns>
    public Polyline ToPolyline(int segments)
    {
        var points = new List<XY>();
        for (var i = 0; i <= segments; i++)
        {
            var t = i / (double)segments;
            points.Add(GetBezierPoint(t));
        }
        return new Polyline(points);
    }

    /// <summary>
    /// Finds the closest point on the Bezier curve to the specified point.
    /// </summary>
    /// <param name="other">The point to find the closest point to.</param>
    /// <param name="segments">The number of segments to approximate the curve.</param>
    /// <returns>The closest point on the Bezier curve.</returns>
    public XY ClosestPoint(XY other, int segments = 100)
    {
        XY? closestPoint = null;
        var closestDistance = double.MaxValue;

        for (var i = 0; i <= segments; i++)
        {
            var t = i / (double)segments;
            var pointOnCurve = GetBezierPoint(t);
            var distance = pointOnCurve.DistanceTo(other);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPoint = pointOnCurve;
            }
        }

        if(closestPoint is null)
        {
            throw new InvalidOperationException("Please provide at least 1 segment.");
        }

        return closestPoint.Value;
    }
}
