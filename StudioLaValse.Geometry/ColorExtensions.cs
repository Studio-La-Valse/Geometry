namespace StudioLaValse.Geometry
{
    public static class ColorExtensions
    {
        public static ColorHSB Average(this IEnumerable<ColorHSB> colors)
        {
            var count = colors.Count();

            if (count == 0)
            {
                return new ColorHSB(0, 0, 0);
            }

            var position = new XY(0, 0);
            var bri = 0d;

            foreach (var color in colors)
            {
                position += color.ToCircularPosition();

                bri += color.Brightness;
            }

            position /= count;
            bri /= count;

            return ColorHSB.FromPosition(position, bri);
        }
    }
}

