using StudioLaValse.Geometry.Private;

namespace StudioLaValse.Geometry
{
    /// <summary>
    /// Represents an immutable color using alpha, hue, saturation and brightness values.
    /// </summary>
    public class ColorAHSB : ColorHSB
    {
        /// <summary>
        /// The alpha of the color.
        /// </summary>
        public double Alpha { get; }

        /// <summary>
        /// Construct an ahsb color from alpha, hue, sat and brightness values.
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="hue"></param>
        /// <param name="sat"></param>
        /// <param name="bri"></param>
        public ColorAHSB(double alpha, double hue, double sat, double bri) : base(hue, sat, bri)
        {
            Alpha = MathUtils.Clamp(alpha, 0, MaxValue);
        }

        /// <summary>
        /// Construct an ASHB color from an alpha value and a <see cref="ColorHSB"/> color.
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="color"></param>
        public ColorAHSB(double alpha, ColorHSB color) : base(color.Hue, color.Saturation, color.Brightness)
        {
            Alpha = MathUtils.Clamp(alpha, 0, MaxValue);
        }

        /// <summary>
        /// Construct a new AHSB color by setting the alpha on this instance.
        /// </summary>
        /// <param name="alpha"></param>
        /// <returns></returns>
        public ColorAHSB SetAlpha(double alpha)
        {
            return new ColorAHSB(alpha, Hue, Saturation, Brightness);
        }

        /// <summary>
        /// Implicitly cast an AHSB color to a <see cref="ColorARGB"/>.
        /// </summary>
        /// <param name="ahsb"></param>
        public static implicit operator ColorARGB(ColorAHSB ahsb) => new ColorARGB((int)(ahsb.Alpha * ColorRGB.MaxValue), ahsb as ColorHSB);
    }
}

