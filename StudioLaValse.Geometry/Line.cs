namespace StudioLaValse.Geometry
{
    public class Line
    {
        public XY Start { get; }
        public XY End { get; }

        public Line(XY start, XY end)
        {
            Start = start;

            End = end;
        }

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

        public double Distance(XY point)
        {
            return DistToSegment(point, Start, End);
        }
    }
}
