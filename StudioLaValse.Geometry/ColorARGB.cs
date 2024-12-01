using StudioLaValse.Geometry.Private;

namespace StudioLaValse.Geometry
{
    /// <summary>
    /// Represents a color using alpha, red, green and blue values.
    /// Values range from 0 to and not including 256.
    /// </summary>
    public readonly struct ColorARGB
    {        
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
        /// White.
        /// </summary>
        public readonly static ColorARGB White = new (255, 255, 255);
        /// <summary>
        /// Black.
        /// </summary>
        public readonly static ColorARGB Black = new (0, 0, 0);

        /// <summary>
        /// Transparant.
        /// </summary>
        public readonly static ColorARGB Transparant = new (0, 0, 0, 0);

        /// <summary>
        /// The alpha value.
        /// </summary>
        public double Alpha { get; }

        /// <summary>
        /// Construct a ColorARGB color from an alpha value and a <see cref="ColorARGB"/>.
        /// </summary>
        public ColorARGB(int red, int green, int blue) 
        {
            Alpha = 1;
            Red = MathUtils.Clamp(red, 0, 255);
            Green = MathUtils.Clamp(green, 0, 255);
            Blue = MathUtils.Clamp(blue, 0, 255);
        }

        /// <summary>
        /// Construct a ColorARGB from alpha, red, green and blue values.
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        public ColorARGB(double alpha, int red, int green, int blue)
        {
            Red = MathUtils.Clamp(red, 0, 255);
            Green = MathUtils.Clamp(green, 0, 255);
            Blue = MathUtils.Clamp(blue, 0, 255);
            Alpha = MathUtils.Clamp(alpha, 0, 1);
        }
    }
}
