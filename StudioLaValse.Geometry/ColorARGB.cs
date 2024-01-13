using StudioLaValse.Geometry.Private;

namespace StudioLaValse.Geometry
{
    public class ColorARGB : ColorRGB
    {
        public static ColorARGB Transparant = new ColorARGB(0, 0, 0, 0);
        new public static ColorARGB White = new ColorARGB(MaxValue, ColorRGB.White);
        new public static ColorARGB Black = new ColorARGB(MaxValue, ColorRGB.Black);

        public int Alpha { get; }

        public ColorARGB(int alpha, ColorRGB color) : base(color.Red, color.Green, color.Blue)
        {
            Alpha = MathUtils.Clamp(alpha, 0, MaxValue);
        }

        public ColorARGB(int alpha, int red, int green, int blue) : base(red, green, blue)
        {
            Alpha = MathUtils.Clamp(alpha, 0, MaxValue);
        }

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
