﻿namespace StudioLaValse.Geometry.Private
{
    internal static class MathUtils
    {
        public static IList<double> Map(IList<double> list, double newMin, double newMax)
        {
            double listMin = list.Min();
            double listMax = list.Max();

            if (listMax - listMin == 0) return list;

            List<double> newList = new List<double>();

            foreach (double value in list)
            {
                double newValue = Map(value, listMin, listMax, newMin, newMax);

                newList.Add((float)newValue);
            }

            return newList;
        }
        public static IList<float> Map(IList<float> list, float newMin, float newMax)
        {
            float listMin = list.Min();
            float listMax = list.Max();

            if (listMax - listMin == 0) return list;

            List<float> newList = new List<float>();

            foreach (float value in list)
            {
                double newValue = Map(value, listMin, listMax, newMin, newMax);

                newList.Add((float)newValue);
            }

            return newList;
        }
        public static double[] SubSampleValues(IEnumerable<double> values, int newLength)
        {
            if (!values.Any())
                return new double[0];

            var originalCount = values.Count();

            if (newLength == originalCount)
                return values.ToArray();

            var newArray = new double[newLength];

            for (int i = 0; i < newLength; i++)
            {
                var indexInOriginalCollection = (int)Math.Floor(Map(i, 0d, newLength, 0, originalCount));

                newArray[i] = values.ElementAt(indexInOriginalCollection);
            }

            return newArray;
        }
        public static float[] SubSampleValues(IEnumerable<float> values, int newLength)
        {
            if (!values.Any())
                return new float[0];

            var originalCount = values.Count();

            if (newLength == originalCount)
                return values.ToArray();

            var newArray = new float[newLength];

            for (int i = 0; i < newLength; i++)
            {
                var indexInOriginalCollection = (int)Math.Floor(Map(i, 0d, newLength, 0, originalCount));

                newArray[i] = values.ElementAt(indexInOriginalCollection);
            }

            return newArray;
        }
        public static double Map(double value, double minStart, double maxStart, double minEnd, double maxEnd)
        {
            double fraction = maxStart - minStart;

            if (fraction == 0)
                throw new ArgumentOutOfRangeException();

            return minEnd + (maxEnd - minEnd) * ((value - minStart) / fraction);
        }
        public static decimal Map(decimal value, decimal minStart, decimal maxStart, decimal minEnd, decimal maxEnd)
        {
            decimal fraction = maxStart - minStart;

            if (fraction == 0)
                throw new ArgumentOutOfRangeException();

            return minEnd + (maxEnd - minEnd) * ((value - minStart) / fraction);
        }
        public static double Clamp(double value, double minValue, double maxValue)
        {
            var trueMin = Math.Min(minValue, maxValue);

            var trueMax = Math.Max(minValue, maxValue);

            return Math.Clamp(value, trueMin, trueMax);
        }
        public static decimal Clamp(decimal value, decimal minValue, decimal maxValue)
        {
            var trueMin = Math.Min(minValue, maxValue);

            var trueMax = Math.Max(minValue, maxValue);

            return Math.Clamp(value, trueMin, trueMax);
        }
        public static int Clamp(int value, int minValue, int maxValue)
        {
            var trueMin = Math.Min(minValue, maxValue);

            var trueMax = Math.Max(minValue, maxValue);

            return Math.Clamp(value, trueMin, trueMax);
        }
        public static double ForcePositiveModulo(double value, double max)
        {
            while (value < 0)
            {
                value += max;
            }

            value %= max;

            return value;
        }
        public static int ForcePositiveModulo(int value, int max)
        {
            while (value < 0)
            {
                value += max;
            }

            value %= max;

            return value;
        }
        public static uint UnsignedModulo(int value, uint max)
        {
            while (value < 0)
            {
                value += (int)max;
            }

            var unsignedValue = (uint)value % max;

            return unsignedValue;
        }
    }
}