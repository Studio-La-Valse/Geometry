using StudioLaValse.Geometry.Private;

namespace StudioLaValse.Geometry;

/// <summary>
/// Color extensions
/// </summary>
public static class ColorExtensions
{
    /// <summary>
    /// Construct a ColorAHSV from a <see cref="ColorARGB"/>.
    /// </summary>
    /// <param name="rgb"></param>
    /// <returns></returns>
    public static ColorAHSV ToColorAHSV(this ColorARGB rgb)
    {
        var r = rgb.Red / (double)255;
        var g = rgb.Green / (double)255;
        var b = rgb.Blue / (double)255;

        var min = Math.Min(Math.Min(r, g), b);
        var max = Math.Max(Math.Max(r, g), b);
        var delta = max - min;

        var bri = max;
        var hue = 0d;
        var sat = max.AlmostEqualTo(0) ? 0 : delta / max;

        if (delta != 0)
        {
            if (r.AlmostEqualTo(max))
            {
                hue = (g - b) / delta + (g < b ? 6 : 0);
            }
            else if (g.AlmostEqualTo(max))
            {
                hue = (b - r) / delta + 2;
            }
            else
            {
                hue = (r - g) / delta + 4;
            }
            hue /= 6;
        }

        return new ColorAHSV(rgb.Alpha, (int)Math.Round(hue * 360), (int)Math.Round(sat * 100), (int)Math.Round(bri * 100));
    }

    /// <summary>
    /// Converto to color argb.
    /// </summary>
    /// <param name="hsv"></param>
    /// <returns></returns>
    public static ColorARGB ToColorARGB(this ColorAHSV hsv)
    {
        HsvToRgb(hsv.Hue, hsv.Saturation / 100d, hsv.Value / 100d, out var r, out var g, out var b);
        var colorARGB = new ColorARGB(hsv.Alpha, r, g, b);
        return colorARGB;
    }

    /// <summary>
    /// Converts a ColorARGB to a <see cref="ColorAHSL"/>.
    /// </summary>
    /// <param name="rgb"></param>
    /// <returns></returns>
    public static ColorAHSL ToColorAHSL(this ColorARGB rgb)
    {
        var r = rgb.Red / (double)255;
        var g = rgb.Green / (double)255;
        var b = rgb.Blue / (double)255;

        var min = Math.Min(Math.Min(r, g), b);
        var max = Math.Max(Math.Max(r, g), b);
        var delta = max - min;

        var l = (max + min) / 2;
        var hue = 0d;
        var sat = 0d;

        if (delta != 0)
        {
            sat = l > 0.5 ? delta / (2.0 - max - min) : delta / (max + min);

            if (r.AlmostEqualTo(max))
            {
                hue = (g - b) / delta + (g < b ? 6 : 0);
            }
            else if (g.AlmostEqualTo(max))
            {
                hue = (b - r) / delta + 2;
            }
            else
            {
                hue = (r - g) / delta + 4;
            }
            hue /= 6;
        }

        return new ColorAHSL(rgb.Alpha, (int)Math.Round(hue * 360), (int)Math.Round(sat * 100), (int)Math.Round(l * 100));
    }

    /// <summary>
    /// Converts a ColorAHSL to a <see cref="ColorARGB"/>.
    /// </summary>
    /// <param name="hsl"></param>
    /// <returns></returns>
    public static ColorARGB ToColorARGB(this ColorAHSL hsl)
    {
        double r, g, b;

        var hue = hsl.Hue / 360.0;
        var sat = hsl.Saturation / 100.0;
        var l = hsl.Lightness / 100.0;

        if (sat.AlmostEqualTo(0))
        {
            r = g = b = l; // achromatic
        }
        else
        {
            var q = l < 0.5 ? l * (1 + sat) : l + sat - l * sat;
            var p = 2 * l - q;
            r = HueToRgb(p, q, hue + 1.0 / 3.0);
            g = HueToRgb(p, q, hue);
            b = HueToRgb(p, q, hue - 1.0 / 3.0);
        }

        return new ColorARGB(
            hsl.Alpha,
            (int)Math.Round(r * 255),
            (int)Math.Round(g * 255),
            (int)Math.Round(b * 255));
    }

    /// <summary>
    /// Converts a ColorAHSV to a <see cref="ColorAHSL"/>.
    /// HSL stands for Hue, Saturation, and Lightness. Lightness ranges from 0 (black) to 1 (white).
    /// HSB stands for Hue, Saturation, and Brightness. Brightness ranges from 0 (black) to 1 (full intensity).
    /// </summary>
    /// <param name="hsb"></param>
    /// <returns></returns>
    public static ColorAHSL ToColorAHSL(this ColorAHSV hsb)
    {
        var h = hsb.Hue;
        var s = hsb.Saturation / 100.0;
        var b = hsb.Value / 100.0;

        var l = b * (1 - s / 2);
        var newS = (l.AlmostEqualTo(0) || l.AlmostEqualTo(1)) ? 0 : (b - l) / Math.Min(l, 1 - l);

        return new ColorAHSL(hsb.Alpha, h, (int)Math.Round(newS * 100), (int)Math.Round(l * 100));
    }

    /// <summary>
    /// Converts a ColorAHSL to a <see cref="ColorAHSV"/>.
    /// HSL stands for Hue, Saturation, and Lightness. Lightness ranges from 0 (black) to 1 (white).
    /// HSB stands for Hue, Saturation, and Brightness. Brightness ranges from 0 (black) to 1 (full intensity).
    /// </summary>
    /// <param name="hsl"></param>
    /// <returns></returns>
    public static ColorAHSV ToColorAHSV(this ColorAHSL hsl)
    {
        var h = hsl.Hue;
        var s = hsl.Saturation / 100.0;
        var l = hsl.Lightness / 100.0;

        var b = l + s * Math.Min(l, 1 - l);
        var newS = (b.AlmostEqualTo(0)) ? 0 : 2 * (b - l) / b;

        return new ColorAHSV(hsl.Alpha, h, (int)Math.Round(newS * 100), (int)Math.Round(b * 100));
    }


    /// <summary>
    /// Convert HSV to RGB
    /// h is from 0-360
    /// s,v values are 0-1
    /// r,g,b values are 0-255
    /// Based upon http://ilab.usc.edu/wiki/index.php/HSV_And_H2SV_Color_Space#HSV_Transformation_C_.2F_C.2B.2B_Code_2
    /// </summary>
    private static void HsvToRgb(double h, double S, double V, out int r, out int g, out int b)
    {
        var H = h;
        while (H < 0) { H += 360; };
        while (H >= 360) { H -= 360; };
        double R, G, B;
        if (V <= 0)
        { R = G = B = 0; }
        else if (S <= 0)
        {
            R = G = B = V;
        }
        else
        {
            var hf = H / 60.0;
            var i = (int)Math.Floor(hf);
            var f = hf - i;
            var pv = V * (1 - S);
            var qv = V * (1 - S * f);
            var tv = V * (1 - S * (1 - f));
            switch (i)
            {

                // Red is the dominant color

                case 0:
                    R = V;
                    G = tv;
                    B = pv;
                    break;

                // Green is the dominant color

                case 1:
                    R = qv;
                    G = V;
                    B = pv;
                    break;
                case 2:
                    R = pv;
                    G = V;
                    B = tv;
                    break;

                // Blue is the dominant color

                case 3:
                    R = pv;
                    G = qv;
                    B = V;
                    break;
                case 4:
                    R = tv;
                    G = pv;
                    B = V;
                    break;

                // Red is the dominant color

                case 5:
                    R = V;
                    G = pv;
                    B = qv;
                    break;

                // Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.

                case 6:
                    R = V;
                    G = tv;
                    B = pv;
                    break;
                case -1:
                    R = V;
                    G = pv;
                    B = qv;
                    break;

                // The color is not defined, we should throw an error.

                default:
                    //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                    R = G = B = V; // Just pretend its black/white
                    break;
            }
        }
        r = Clamp((int)Math.Round(R * 255.0));
        g = Clamp((int)Math.Round(G * 255.0));
        b = Clamp((int)Math.Round(B * 255.0));
    }

    /// <summary>
    /// Clamp a value to 0-255
    /// </summary>
    private static int Clamp(int i)
    {
        if (i < 0)
        {
            return 0;
        }

        return i > 255 ? 255 : i;
    }

    private static double HueToRgb(double p, double q, double t)
    {
        if (t < 0)
        {
            t += 1;
        }

        if (t > 1)
        {
            t -= 1;
        }

        if (t < 1.0 / 6.0)
        {
            return p + (q - p) * 6 * t;
        }

        if (t < 1.0 / 2.0)
        {
            return q;
        }

        return t < 2.0 / 3.0 ? p + (q - p) * (2.0 / 3.0 - t) * 6 : p;
    }
}
