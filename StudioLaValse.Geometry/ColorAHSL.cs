using StudioLaValse.Geometry.Private;

namespace StudioLaValse.Geometry
{
    /// <summary>
    /// Represents an immutable color using alpha, hue, saturation and lightness values.
    /// </summary>
    public readonly struct ColorAHSL
    {
        /// <summary>
        /// Alpha.
        /// </summary>
        public double Alpha { get; }
        /// <summary>
        /// Hue.
        /// </summary>
        public int Hue { get; }
        /// <summary>
        /// Saturation.
        /// </summary>
        public int Saturation { get; }
        /// <summary>
        /// Lightness.
        /// </summary>
        public int Lightness { get; }

        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="hue"></param>
        /// <param name="saturation"></param>
        /// <param name="lightness"></param>
        public ColorAHSL(double alpha, int hue, int saturation, int lightness)
        {
            Hue = MathUtils.ForcePositiveModulo(hue, 360);

            Saturation = MathUtils.Clamp(saturation, 0, 100);

            Lightness = MathUtils.Clamp(lightness, 0, 100);

            Alpha = MathUtils.Clamp(alpha, 0, 1);
        }

        /// <summary>
        /// Construct to color hsl with alpha set to 1.
        /// </summary>
        /// <param name="hue"></param>
        /// <param name="saturation"></param>
        /// <param name="lightness"></param>
        public ColorAHSL(int hue, int saturation, int lightness)
        {
            Hue = MathUtils.ForcePositiveModulo(hue, 360);

            Saturation = MathUtils.Clamp(saturation, 0, 100);

            Lightness = MathUtils.Clamp(lightness, 0, 100);

            Alpha = 1;
        }
    }
}

