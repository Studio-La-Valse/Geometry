namespace StudioLaValse.Geometry
{
    public class XY
    {
        public double X { get; }
        public double Y { get; }

        public XY(double x, double y)
        {
            X = x;
            Y = y;
        }
        public static XY Average(List<XY> positions)
        {
            var x = 0d;
            var y = 0d;

            foreach (var position in positions)
            {
                x += position.X;
                y += position.Y;
            }

            x /= positions.Count;
            y /= positions.Count;

            return new XY(x, y);
        }


        public double DistanceTo(XY secondPoint)
        {
            return Math.Sqrt(Math.Pow(secondPoint.X - X, 2) + Math.Pow(secondPoint.Y - Y, 2));
        }
        public double Length()
        {
            return DistanceTo(new XY(0, 0));
        }
        public XY Normalize()
        {
            return this / Length();
        }
        public XY Move(double length, double angle)
        {
            var o = Math.Sin(angle) * length;

            var a = Math.Cos(angle) * length;

            return new XY(X + a, Y + o);
        }
        public static XY operator +(XY left, XY right)
        {
            return new XY(left.X + right.X, left.Y + right.Y);
        }
        public static XY operator -(XY left, XY right)
        {
            return new XY(left.X - right.X, left.Y - right.Y);
        }
        public static XY operator /(XY left, double right)
        {
            if (right == 0) throw new DivideByZeroException();

            return new XY(left.X / right, left.Y / right);
        }
        public static XY operator *(XY left, double right)
        {
            return new XY(left.X * right, left.Y * right);
        }


        //public static implicit operator Point(XY xy) => new Point(xy.X, xy.Y);
        //public static implicit operator XY(Point point) => new XY(point.X, point.Y);
    }
}
