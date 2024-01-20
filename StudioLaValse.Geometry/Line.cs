namespace StudioLaValse.Geometry
{
    /// <summary>
    /// Represents an immutable line.
    /// </summary>
    public class Line
    {
        /// <summary>
        /// The start point.
        /// </summary>
        public XY Start { get; }
        /// <summary>
        /// The end point.
        /// </summary>
        public XY End { get; }

        /// <summary>
        /// Construct a line from two points.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public Line(XY start, XY end)
        {
            Start = start;

            End = end;
        }

        /// <summary>
        /// Construct a line from coordinates.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        public Line(double x1, double y1, double x2, double y2)
        {
            Start = new XY(x1, y1);
            End = new XY(x2, y2);
        }

        private static double Sqr(double x) { return x * x; }
        private static double Dist2(XY v, XY w) { return Sqr(v.X - w.X) + Sqr(v.Y - w.Y); }
        private static double DistToSegmentSquared(XY p, XY v, XY w)
        {
            var l2 = Dist2(v, w);
            if (l2 == 0)
            {
                return Dist2(p, v);
            }

            var t = ((p.X - v.X) * (w.X - v.X) + (p.Y - v.Y) * (w.Y - v.Y)) / l2;
            t = Math.Max(0, Math.Min(1, t));
            return Dist2(p, new XY(x: v.X + t * (w.X - v.X), y: v.Y + t * (w.Y - v.Y)));
        }
        private static double DistToSegment(XY p, XY v, XY w) { return Math.Sqrt(DistToSegmentSquared(p, v, w)); }

        /// <summary>
        /// Calculates the smallest distance between the specified point and this line.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public double Distance(XY point)
        {
            return DistToSegment(point, Start, End);
        }
    }
}
