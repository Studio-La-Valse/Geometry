using StudioLaValse.Geometry.Private;

namespace StudioLaValse.Geometry
{
    /// <summary>
    /// Represents a color using alpha, red, green and blue values.
    /// Values range from 0 to and including 255.
    /// </summary>
    public class ColorARGB : ColorRGB
    {
        /// <summary>
        /// Transparant.
        /// </summary>
        public static ColorARGB Transparant = new ColorARGB(0, 0, 0, 0);
        /// <summary>
        /// White.
        /// </summary>
        new public static ColorARGB White = new ColorARGB(MaxValue, ColorRGB.White);
        /// <summary>
        /// Black.
        /// </summary>
        new public static ColorARGB Black = new ColorARGB(MaxValue, ColorRGB.Black);

        /// <summary>
        /// The alpha value.
        /// </summary>
        public int Alpha { get; }

        /// <summary>
        /// Construct a ColorARGB color from an alpha value and a <see cref="ColorRGB"/>.
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="color"></param>
        public ColorARGB(int alpha, ColorRGB color) : base(color.Red, color.Green, color.Blue)
        {
            Alpha = MathUtils.Clamp(alpha, 0, MaxValue);
        }

        /// <summary>
        /// Construct a ColorARGB from alpha, red, green and blue values.
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        public ColorARGB(int alpha, int red, int green, int blue) : base(red, green, blue)
        {
            Alpha = MathUtils.Clamp(alpha, 0, MaxValue);
        }

        /// <summary>
        /// Determines wether this instance has other alpha, red, green and blue values from the specified other <see cref="ColorARGB"/>.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool IsDifferent(ColorARGB other)
        {
            if (other is null)
                return true;

            if (Alpha != other.Alpha)
                return true;

            return base.IsDifferent(other);
        }
    }
}
