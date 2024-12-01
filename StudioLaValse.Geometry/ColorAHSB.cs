using StudioLaValse.Geometry.Private;
using System;

namespace StudioLaValse.Geometry
{
    /// <summary>
    /// Represents an immutable color using alpha, hue, saturation and brightness values.
    /// </summary>
    public readonly struct ColorAHSV
    {        
        /// <summary>
        /// Hue in degrees.
        /// </summary>
        public int Hue { get; }
        /// <summary>
        /// Saturation in percentage.
        /// </summary>
        public int Saturation { get; }
        /// <summary>
        /// Brightness in percentage.
        /// </summary>
        public int Value { get; }
        /// <summary>
        /// The alpha of the color.
        /// </summary>
        public double Alpha { get; }

        /// <summary>
        /// Construct an AHSV color from hue, sat and brightness values.
        /// Alpha defaults to 1.
        /// </summary>
        /// <param name="hue"></param>
        /// <param name="sat"></param>
        /// <param name="bri"></param>
        public ColorAHSV(int hue, int sat, int bri) 
        {
            Hue = MathUtils.ForcePositiveModulo(hue, 360);

            Saturation = MathUtils.Clamp(sat, 0, 100);

            Value = MathUtils.Clamp(bri, 0, 100);

            Alpha = 1;
        }

        /// <summary>
        /// Construct an AHSV color from alpha, hue, sat and brightness values.
        /// </summary>
        /// <param name="alpha"></param>
        /// <param name="hue"></param>
        /// <param name="sat"></param>
        /// <param name="bri"></param>
        public ColorAHSV(double alpha, int hue, int sat, int bri)
        {
            Hue = MathUtils.ForcePositiveModulo(hue, 360);

            Saturation = MathUtils.Clamp(sat, 0, 100);

            Value = MathUtils.Clamp(bri, 0, 100);

            Alpha = MathUtils.Clamp(alpha, 0, 1);
        }
    }
}

