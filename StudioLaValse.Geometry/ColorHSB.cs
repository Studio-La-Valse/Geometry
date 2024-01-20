using StudioLaValse.Geometry.Private;

namespace StudioLaValse.Geometry
{
    /// <summary>
    /// Represents a color from hue, saturation and brightness values.
    /// Values range from 0 to 1.
    /// </summary>
    public class ColorHSB
    {
        /// <summary>
        /// The max value.
        /// </summary>
        public const double MaxValue = 1;

        /// <summary>
        /// Hue.
        /// </summary>
        public double Hue { get; }
        /// <summary>
        /// Saturation.
        /// </summary>
        public double Saturation { get; }
        /// <summary>
        /// Brightness.
        /// </summary>
        public double Brightness { get; }

        /// <summary>
        /// Construct a color from hue, saturation and brightness values.
        /// </summary>
        /// <param name="hue"></param>
        /// <param name="sat"></param>
        /// <param name="bri"></param>
        public ColorHSB(double hue, double sat, double bri)
        {
            Hue = MathUtils.ForcePositiveModulo(hue, MaxValue);

            Saturation = MathUtils.Clamp(sat, 0, MaxValue);

            Brightness = MathUtils.Clamp(bri, 0, MaxValue);
        }

        /// <summary>
        /// Construct a ColorHSB from a <see cref="ColorRGB"/>.
        /// </summary>
        /// <param name="rgb"></param>
        /// <returns></returns>
        public static ColorHSB FromRGB(ColorRGB rgb)
        {
            var r = rgb.Red / (double)ColorRGB.MaxValue;
            var g = rgb.Green / (double)ColorRGB.MaxValue;
            var b = rgb.Blue / (double)ColorRGB.MaxValue;

            var min = Math.Min(Math.Min(r, g), b);
            var max = Math.Max(Math.Max(r, g), b);
            var delta = max - min;

            var bri = (max + min) / 2;
            var hue = 0d;
            var sat = 0d;

            if (delta == 0)
            {
                // do nothing
            }
            else
            {
                sat = (bri <= 0.5) ? (delta / (max + min)) : (delta / (2 - max - min));

                if (r == max)
                {
                    hue = ((g - b) / 6) / delta;
                }
                else if (g == max)
                {
                    hue = (1.0f / 3) + ((b - r) / 6) / delta;
                }
                else
                {
                    hue = (2.0f / 3) + ((r - g) / 6) / delta;
                }

                if (hue < 0)
                    hue += 1;
                if (hue > 1)
                    hue -= 1;
            }

            return new ColorHSB(hue, sat, bri);
        }
        /// <summary>
        /// Construct a ColorHSB from a position in a circle.
        /// Red hues start at the top and rotates counter clockwise.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="brightness"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static ColorHSB FromPosition(XY position, double brightness, double radius = 1)
        {
            var angle = Math.Atan2(position.X, position.Y);

            var hue = MathUtils.Map(angle, 0, Math.PI * 2, 0, MaxValue);

            var sat = MathUtils.Map(new XY(0, 0).DistanceTo(position), 0, radius, 0, MaxValue);

            return new ColorHSB(hue, sat, brightness);
        }
        /// <summary>
        /// Construct a point in a color circle from this color.
        /// </summary>
        /// <param name="radius"></param>
        /// <returns></returns>
        public XY ToCircularPosition(double radius = 1)
        {
            var angle = MathUtils.Map(Hue, 0, MaxValue, 0, Math.PI * 2);

            var dist = MathUtils.Map(Saturation, 0, MaxValue, 0, radius);

            var xPos = Math.Sin(angle) * dist;

            var yPos = Math.Cos(angle) * dist;

            return new XY(xPos, yPos);
        }

        /// <summary>
        /// Construct a new ColorHSB from setting the brightness on this color.
        /// </summary>
        /// <param name="brightness"></param>
        /// <returns></returns>
        public ColorHSB SetBrightness(double brightness)
        {
            return new ColorHSB(Hue, Saturation, brightness);
        }
        /// <summary>
        /// Construct a new ColorHSB by setting the saturation on this color.
        /// </summary>
        /// <param name="saturation"></param>
        /// <returns></returns>
        public ColorHSB SetSaturation(double saturation)
        {
            return new ColorHSB(Hue, saturation, Brightness);
        }


        /// <summary>
        /// Cast to a <see cref="ColorRGB"/>
        /// </summary>
        /// <param name="hsb"></param>
        public static implicit operator ColorRGB(ColorHSB hsb) => ColorRGB.FromHSB(hsb);
        /// <summary>
        /// Cast to a <see cref="ColorARGB"/>
        /// </summary>
        /// <param name="hsb"></param>
        public static implicit operator ColorARGB(ColorHSB hsb) => new ColorARGB(255, ColorRGB.FromHSB(hsb));
        /// <summary>
        /// Cast to a <see cref="ColorHSB"/>
        /// </summary>
        /// <param name="rgb"></param>
        public static implicit operator ColorHSB(ColorRGB rgb) => FromRGB(rgb);
    }
}

