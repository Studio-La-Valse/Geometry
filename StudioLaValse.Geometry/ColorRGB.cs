using StudioLaValse.Geometry.Private;

namespace StudioLaValse.Geometry
{
    /// <summary>
    /// Represents an immutable color using red, green and blue values.
    /// Values range from 0 to and including 255.
    /// </summary>
    public class ColorRGB
    {
        /// <summary>
        /// The max value.
        /// </summary>
        public const int MaxValue = 255;
        /// <summary>
        /// White.
        /// </summary>
        public static ColorRGB White = new ColorRGB(MaxValue, MaxValue, MaxValue);
        /// <summary>
        /// Black.
        /// </summary>
        public static ColorRGB Black = new ColorRGB(0, 0, 0);

        /// <summary>
        /// The value of the red channel.
        /// </summary>
        public int Red { get; }
        /// <summary>
        /// The value of the green channel.
        /// </summary>
        public int Green { get; }
        /// <summary>
        /// The value of the blue channel.
        /// </summary>
        public int Blue { get; }

        /// <summary>
        /// Construct a color from red green an blue values.
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        public ColorRGB(int r, int g, int b)
        {
            Red = MathUtils.Clamp(r, 0, MaxValue);
            Green = MathUtils.Clamp(g, 0, MaxValue);
            Blue = MathUtils.Clamp(b, 0, MaxValue);
        }

        /// <summary>
        /// Construct a ColorRGB from a <see cref="ColorHSB"/>.
        /// </summary>
        /// <param name="hsb"></param>
        /// <returns></returns>
        public static ColorRGB FromHSB(ColorHSB hsb)
        {
            double r, g, b;

            if (hsb.Saturation == 0.0d)
            {
                r = g = b = hsb.Brightness;
            }
            else
            {
                var q = hsb.Brightness < 0.5d ? hsb.Brightness * (1.0d + hsb.Saturation) : hsb.Brightness + hsb.Saturation - hsb.Brightness * hsb.Saturation;
                var p = 2.0d * hsb.Brightness - q;

                r = HueToRgb(p, q, hsb.Hue + 1.0d / 3.0d);
                g = HueToRgb(p, q, hsb.Hue);
                b = HueToRgb(p, q, hsb.Hue - 1.0d / 3.0d);
            }

            return new ColorRGB((int)(r * MaxValue), (int)(g * MaxValue), (int)(b * MaxValue));
        }

        private static double HueToRgb(double p, double q, double t)
        {
            if (t < 0.0d) t += 1.0d;
            if (t > 1.0d) t -= 1.0d;
            if (t < 1.0d / 6.0d) return p + (q - p) * 6.0d * t;
            if (t < 1.0d / 2.0d) return q;
            if (t < 2.0d / 3.0d) return p + (q - p) * (2.0d / 3.0d - t) * 6.0d;

            return p;
        }

        /// <summary>
        /// Returns true if any of the red green and blue channels differ from the other color.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool IsDifferent(ColorRGB other)
        {
            if (other is null)
                return true;

            if (other == this)
                return false;

            if (Red != other.Red)
                return true;

            if (Blue != other.Blue)
                return true;

            if (Green != other.Green)
                return true;

            return false;
        }

        /// <summary>
        /// Cast a <see cref="ColorRGB"/> to a <see cref="ColorHSB"/>
        /// </summary>
        /// <param name="rgb"></param>
        public static implicit operator ColorHSB(ColorRGB rgb) => ColorHSB.FromRGB(rgb);
    }
}
