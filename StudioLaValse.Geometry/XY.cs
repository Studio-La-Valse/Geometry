namespace StudioLaValse.Geometry
{
    /// <summary>
    /// Represents an immutable coordinate.
    /// </summary>
    public readonly struct XY
    {
        /// <summary>
        /// The X-coordinate.
        /// </summary>
        public double X { get; }
        /// <summary>
        /// The Y-coordinate.
        /// </summary>
        public double Y { get; }

        /// <summary>
        /// Construct a point from x- and y values.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public XY(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Averages a set of points.
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
        public static XY Average(IEnumerable<XY> positions)
        {
            var x = 0d;
            var y = 0d;

            var count = 0;
            foreach (var position in positions)
            {
                x += position.X;
                y += position.Y;
                count++;
            }

            if(count == 0)
            {
                throw new InvalidOperationException("No points provided.");
            }
            x /= count;
            y /= count;

            return new XY(x, y);
        }

        /// <summary>
        /// Calculates the distance between this and the other point.
        /// </summary>
        /// <param name="secondPoint"></param>
        /// <returns></returns>
        public double DistanceTo(XY secondPoint)
        {
            return Math.Sqrt(Math.Pow(secondPoint.X - X, 2) + Math.Pow(secondPoint.Y - Y, 2));
        }
        /// <summary>
        /// Calculates the distance between this point and (0, 0).
        /// </summary>
        /// <returns></returns>
        public double Length()
        {
            return DistanceTo(new XY(0, 0));
        }
        /// <summary>
        /// Casts this point to a vector from (0, 0) and normalizes the vector.
        /// </summary>
        /// <returns></returns>
        public XY Normalize()
        {
            return this / Length();
        }
        /// <summary>
        /// Construct a new point by moving this point.
        /// </summary>
        /// <param name="length"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public XY Move(double length, double angle)
        {
            var o = Math.Sin(angle) * length;

            var a = Math.Cos(angle) * length;

            return new XY(X + a, Y + o);
        }
        /// <summary>
        /// Add two points.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static XY operator +(XY left, XY right)
        {
            return new XY(left.X + right.X, left.Y + right.Y);
        }
        /// <summary>
        /// Subtract the second point from the first.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static XY operator -(XY left, XY right)
        {
            return new XY(left.X - right.X, left.Y - right.Y);
        }
        /// <summary>
        /// Casts the point to a vector and divides the length of the vector by the specified value.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        /// <exception cref="DivideByZeroException"></exception>
        public static XY operator /(XY left, double right)
        {
            return right.AlmostEqualTo(0) ? throw new DivideByZeroException() : new XY(left.X / right, left.Y / right);
        }
        /// <summary>
        /// Casts the point to a vector and multiplies the length of the vector by the specified value.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static XY operator *(XY left, double right)
        {
            return new XY(left.X * right, left.Y * right);
        }
    }
}
