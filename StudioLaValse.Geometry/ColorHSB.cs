using StudioLaValse.Geometry.Private;

namespace StudioLaValse.Geometry
{
    public class ColorHSB
    {
        public const double MaxValue = 1;

        public double Hue { get; }
        public double Saturation { get; }
        public double Brightness { get; }

        public ColorHSB(double hue, double sat, double bri)
        {
            Hue = MathUtils.ForcePositiveModulo(hue, MaxValue);

            Saturation = MathUtils.Clamp(sat, 0, MaxValue);

            Brightness = MathUtils.Clamp(bri, 0, MaxValue);
        }


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
        public static ColorHSB FromPosition(XY position, double brightness, double radius = 1)
        {
            var angle = Math.Atan2(position.X, position.Y);

            var hue = MathUtils.Map(angle, 0, Math.PI * 2, 0, MaxValue);

            var sat = MathUtils.Map(new XY(0, 0).DistanceTo(position), 0, radius, 0, MaxValue);

            return new ColorHSB(hue, sat, brightness);
        }
        public XY ToCircularPosition(double radius = 1)
        {
            var angle = MathUtils.Map(Hue, 0, MaxValue, 0, Math.PI * 2);

            var dist = MathUtils.Map(Saturation, 0, MaxValue, 0, radius);

            var xPos = Math.Sin(angle) * dist;

            var yPos = Math.Cos(angle) * dist;

            return new XY(xPos, yPos);
        }


        public ColorHSB SetBrightness(double brightness)
        {
            return new ColorHSB(Hue, Saturation, brightness);
        }
        public ColorHSB SetSaturation(double saturation)
        {
            return new ColorHSB(Hue, saturation, Brightness);
        }



        public static implicit operator ColorRGB(ColorHSB hsb) => ColorRGB.FromHSB(hsb);
        public static implicit operator ColorARGB(ColorHSB hsb) => new ColorARGB(255, ColorRGB.FromHSB(hsb));
        public static implicit operator ColorHSB(ColorRGB rgb) => FromRGB(rgb);
    }
}

