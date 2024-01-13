using StudioLaValse.Geometry.Private;

namespace StudioLaValse.Geometry
{
    public class ColorAHSB : ColorHSB
    {
        public double Alpha { get; }

        public ColorAHSB(double alpha, double hue, double sat, double bri) : base(hue, sat, bri)
        {
            Alpha = MathUtils.Clamp(alpha, 0, MaxValue);
        }
        public ColorAHSB(double alpha, ColorHSB color) : base(color.Hue, color.Saturation, color.Brightness)
        {
            Alpha = MathUtils.Clamp(alpha, 0, MaxValue);
        }

        public ColorAHSB SetAlpha(double alpha)
        {
            return new ColorAHSB(alpha, Hue, Saturation, Brightness);
        }

        public static implicit operator ColorARGB(ColorAHSB ahsb) => new ColorARGB((int)(ahsb.Alpha * ColorRGB.MaxValue), ahsb as ColorHSB);
    }
}

