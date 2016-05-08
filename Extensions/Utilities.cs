using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Extensions
{
    public static class Utilities
    {
        private static Random any_random = new Random(1);

        private static NumberFormatInfo NumberFormat = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();

        public static IEnumerable<int> ErastotenesSieve(int limit)
        {
            List<int> list = new List<int>
            {
                2,
                3,
                5
            };
            BitArray bitArray = new BitArray(limit + 1, true);
            bitArray[0] = (bitArray[1] = false);
            int num = 2;
            while ((double)num <= Math.Sqrt((double)limit))
            {
                if (bitArray[num])
                {
                    for (int i = num * num; i <= limit; i += num)
                    {
                        bitArray[i] = false;
                    }
                }
                num++;
            }
            for (int j = list.Last<int>() + 2; j <= limit; j++)
            {
                if (bitArray[j])
                {
                    list.Add(j);
                }
            }
            return list;
        }

        public static bool IsPrime(this int n)
        {
            if (n < 2)
            {
                return false;
            }
            if (n == 2 || n == 3)
            {
                return true;
            }
            if (n % 2 == 0 || n % 3 == 0)
            {
                return false;
            }
            int num = (int)Math.Sqrt((double)n);
            for (int i = 5; i <= num; i += 6)
            {
                if (n % i == 0 || n % (i + 2) == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static double ToDegrees(this double Radians)
        {
            return Radians * 0.017453292519943295;
        }

        public static double ToRadians(this double Degrees)
        {
            return Degrees / 57.295779513082323;
        }

        public static double[] ConstructLine(double[] StartingPointXY, int Lenght, int AngleDegrees)
        {
            return new double[]
            {
                StartingPointXY[0],
                StartingPointXY[1],
                StartingPointXY[0] + (double)Lenght * Math.Cos((double)AngleDegrees),
                StartingPointXY[1] + (double)Lenght * Math.Sin((double)AngleDegrees)
            };
        }

        public static double InterpolatePoints(double[] p1, double[] p2, double x)
        {
            return p1[1] + ((x - p1[0]) * p2[1] - (x - p1[0]) * p1[1]) / (p2[0] - p1[0]);
        }

        public static double ManhattanDistance(this IEnumerable<double> source, IEnumerable<double> other)
        {
            double num = 0.0;
            using (IEnumerator<double> enumerator = source.GetEnumerator())
            {
                using (IEnumerator<double> enumerator2 = other.GetEnumerator())
                {
                    while (enumerator.MoveNext() && enumerator2.MoveNext())
                    {
                        num += Math.Abs(enumerator.Current - enumerator2.Current);
                    }
                }
            }
            return num;
        }

        public static double EuclideanDistance(this IEnumerable<double> source, IEnumerable<double> other)
        {
            double num = 0.0;
            using (IEnumerator<double> enumerator = source.GetEnumerator())
            {
                using (IEnumerator<double> enumerator2 = other.GetEnumerator())
                {
                    while (enumerator.MoveNext() && enumerator2.MoveNext())
                    {
                        num += Math.Pow(enumerator.Current - enumerator2.Current, 2.0);
                    }
                }
            }
            return Math.Sqrt(num);
        }

        public static double MSE(this IEnumerable<double> source, IEnumerable<double> other)
        {
            double num = 0.0;
            using (IEnumerator<double> enumerator = source.GetEnumerator())
            {
                using (IEnumerator<double> enumerator2 = other.GetEnumerator())
                {
                    while (enumerator.MoveNext() && enumerator2.MoveNext())
                    {
                        num += Math.Pow(enumerator.Current - enumerator2.Current, 2.0);
                    }
                }
            }
            return Math.Sqrt(num);
        }

        public static IEnumerable<int> GetDifferences(this IEnumerable<int> data)
        {
            int num = data.First<int>();
            foreach (int current in data.Skip(1))
            {
                yield return current - num;
                num = current;
            }
            yield break;
        }

        public static int HashBitsLSB(this IEnumerable<bool> bits)
        {
            int num = 0;
            int num2 = 1;
            foreach (bool current in bits)
            {
                num += (current ? num2 : 0);
                num2 *= 2;
            }
            return num;
        }

        public static int HashBitsMSB(this IEnumerable<bool> bits)
        {
            return bits.Reverse<bool>().HashBitsLSB();
        }

        public static IEnumerable<byte> GetBytesMSB(this int number, byte InBase)
        {
            Stack<byte> stack = new Stack<byte>(32);
            while (number >= (int)InBase)
            {
                int num;
                number = Math.DivRem(number, (int)InBase, out num);
                stack.Push((byte)num);
            }
            stack.Push((byte)number);
            return stack;
        }

        public static IEnumerable<byte> GetBytesLSB(this int number, byte InBase)
        {
            List<byte> list = new List<byte>(32);
            while (number >= (int)InBase)
            {
                int num;
                number = Math.DivRem(number, (int)InBase, out num);
                list.Add((byte)num);
            }
            list.Add((byte)number);
            return list;
        }

        public static IEnumerable<byte> GetBytesMSB(this long number, byte InBase)
        {
            Stack<byte> stack = new Stack<byte>(64);
            while (number >= (long)((ulong)InBase))
            {
                long num;
                number = Math.DivRem(number, (long)((ulong)InBase), out num);
                stack.Push((byte)num);
            }
            stack.Push((byte)number);
            return stack;
        }

        public static IEnumerable<byte> GetBytesLSB(this long number, byte InBase)
        {
            List<byte> list = new List<byte>(64);
            while (number >= (long)((ulong)InBase))
            {
                long num;
                number = Math.DivRem(number, (long)((ulong)InBase), out num);
                list.Add((byte)num);
            }
            list.Add((byte)number);
            return list;
        }

        public static long ToBase(this int number, byte TargetBase)
        {
            IEnumerable<byte> bytesMSB = number.GetBytesMSB(TargetBase);
            number = 0;
            int num = 1;
            foreach (byte current in bytesMSB)
            {
                number += (int)current * num;
                num *= 10;
            }
            return (long)number;
        }

        public static BitArray ToBitArrayLSB(this int number)
        {
            BitArray bitArray = new BitArray(32);
            byte b = 0;
            while ((int)b < bitArray.Count)
            {
                bitArray[(int)b] = ((number & 1 << (int)b) != 0);
                b += 1;
            }
            return bitArray;
        }

        public static BitArray ToBitArrayMSB(this int number)
        {
            BitArray bitArray = new BitArray(32);
            byte b = 0;
            while ((int)b < bitArray.Count)
            {
                bitArray[(int)(31 - b)] = ((number & 1 << (int)b) != 0);
                b += 1;
            }
            return bitArray;
        }

        public static BitArray ToBitArrayLSB(this long number)
        {
            BitArray bitArray = new BitArray(64);
            byte b = 0;
            while ((int)b < bitArray.Count)
            {
                bitArray[(int)b] = ((number & 1L << (int)b) != 0L);
                b += 1;
            }
            return bitArray;
        }

        public static BitArray ToBitArrayMSB(this long number)
        {
            BitArray bitArray = new BitArray(64);
            byte b = 0;
            while ((int)b < bitArray.Count)
            {
                bitArray[(int)(63 - b)] = ((number & 1L << (int)b) != 0L);
                b += 1;
            }
            return bitArray;
        }

        public static IEnumerable<bool> ToBoolsLSB(this long number)
        {
            List<bool> list = new List<bool>(64);
            for (byte b = 0; b < 64; b += 1)
            {
                list.Add((number & 1L << (int)b) != 0L);
            }
            return list;
        }

        public static IEnumerable<bool> ToBoolsMSB(this long number)
        {
            Stack<bool> stack = new Stack<bool>(64);
            for (byte b = 0; b < 64; b += 1)
            {
                stack.Push((number & 1L << (int)b) != 0L);
            }
            return stack;
        }

        public static bool GetBit(this byte b, byte bitNumber)
        {
            return ((int)b & 1 << (int)bitNumber) != 0;
        }

        public static bool GetBit_comp_(this byte b, byte bitNumber)
        {
            return (b >> (int)bitNumber & 1) != 0;
        }

        public static long ToBaseFrom_comp_(this long number, byte FromBase, byte ToBase)
        {
            return Convert.ToInt64(Convert.ToString(Convert.ToInt64(number.ToString(), (int)FromBase), (int)ToBase));
        }

        public static long ToBaseFrom_comp_(this int number, byte FromBase, byte ToBase)
        {
            return Convert.ToInt64(Convert.ToString(Convert.ToInt64(number.ToString(), (int)FromBase), (int)ToBase));
        }

        public static long ToLongInBase(this long number, byte ToBase)
        {
            return Convert.ToInt64(Convert.ToString(number, (int)ToBase), 10);
        }

        public static long ToLongInBase(this int number, byte ToBase)
        {
            return Convert.ToInt64(Convert.ToString(number, (int)ToBase), 10);
        }

        public static byte GetByte(this int number, byte NthByte)
        {
            return (byte)(number >> (int)(8 * NthByte) & 255);
        }

        public static byte[] GetBytes(this int number)
        {
            return new byte[]
            {
                (byte)(number & 255),
                (byte)(number >> 8 & 255),
                (byte)(number >> 16 & 255),
                (byte)(number >> 24 & 255)
            };
        }

        public static byte GetByte(this short number, byte NthByte)
        {
            return (byte)(number >> (int)(8 * NthByte) & 255);
        }

        public static IEnumerable<KeyValuePair<string, double>> SortByValue(this IEnumerable<KeyValuePair<string, double>> dictionary)
        {
            return from pair in dictionary
                   orderby pair.Value descending
                   select pair;
        }

        public static string[] ToItems(this IEnumerable<KeyValuePair<string, double>> input, string format = "{0}\t\t{1}%")
        {
            List<string> list = new List<string>();
            Utilities.NumberFormat.NumberGroupSeparator = " ";
            foreach (KeyValuePair<string, double> current in input)
            {
                string arg = Math.Round(current.Value, 2).ToString("#,#.00", Utilities.NumberFormat);
                list.Add(string.Format(format, current.Key, arg));
            }
            return list.ToArray();
        }

        public static string[] ToItems<S, T>(this IEnumerable<KeyValuePair<S, T>> input, string format = "{0}\t\t{1}")
        {
            List<string> list = new List<string>();
            foreach (KeyValuePair<S, T> current in input)
            {
                list.Add(string.Format(format, current.Key, current.Value));
            }
            return list.ToArray();
        }

        public static byte RandByte(byte upper_limit = 255)
        {
            return (byte)Utilities.any_random.Next((int)upper_limit);
        }

        public static int Rand(int MaxValue = 2147483647)
        {
            return Utilities.any_random.Next(MaxValue);
        }

        public static double RandNormal(double mean, double SD = 1.0)
        {
            double num = Math.Sqrt(-2.0 * Math.Log(Utilities.any_random.NextDouble())) * Math.Sin(6.2831853071795862 * Utilities.any_random.NextDouble());
            return mean + SD * num;
        }

        public static int AsColor(byte Red, byte Green, byte Blue, byte Alpha = 255)
        {
            return (int)Red + ((int)Green << 8) + ((int)Blue << 16) + ((int)Alpha << 24);
        }

        public static byte[] AsColor(this int argb)
        {
            return new byte[]
            {
                (byte)(argb >> 24 & 255),
                (byte)(argb >> 16 & 255),
                (byte)(argb >> 8 & 255),
                (byte)(argb & 255)
            };
        }

        public static byte[] HSVtoRGB(double hue, double saturation, double value)
        {
            int num = Convert.ToInt32(Math.Floor(hue / 60.0)) % 6;
            double num2 = hue / 60.0 - Math.Floor(hue / 60.0);
            value *= 255.0;
            byte b = Convert.ToByte(value);
            byte b2 = Convert.ToByte(value * (1.0 - saturation));
            byte b3 = Convert.ToByte(value * (1.0 - num2 * saturation));
            byte b4 = Convert.ToByte(value * (1.0 - (1.0 - num2) * saturation));
            switch (num)
            {
                case 0:
                    return new byte[]
                    {
                    255,
                    b,
                    b4,
                    b2
                    };
                case 1:
                    return new byte[]
                    {
                    255,
                    b3,
                    b,
                    b2
                    };
                case 2:
                    return new byte[]
                    {
                    255,
                    b2,
                    b,
                    b4
                    };
                case 3:
                    return new byte[]
                    {
                    255,
                    b2,
                    b3,
                    b
                    };
                case 4:
                    return new byte[]
                    {
                    255,
                    b4,
                    b2,
                    b
                    };
                default:
                    return new byte[]
                    {
                    255,
                    b,
                    b2,
                    b3
                    };
            }
        }

        public static string AsMemory(this decimal bytes, string NumberSeparator = " ")
        {
            Utilities.NumberFormat.NumberGroupSeparator = NumberSeparator;
            if (bytes < 1024m)
            {
                return string.Format("{0} bytes", bytes.ToString(Utilities.NumberFormat));
            }
            if (bytes < 1048576m)
            {
                return string.Format("{0} Kb", Math.Round(bytes / 1024m, 1).ToString(Utilities.NumberFormat));
            }
            if (bytes < 1073741824m)
            {
                return string.Format("{0} Mb", Math.Round(bytes / 1048576m, 1).ToString(Utilities.NumberFormat));
            }
            return string.Format("{0} Gb", Math.Round(bytes / 1073741824m, 1).ToString(Utilities.NumberFormat));
        }

        public static string AsMemory(this ulong bytes, string NumberSeparator = " ")
        {
            return bytes.AsMemory(NumberSeparator);
        }

        public static string AsMemory(this long bytes, string NumberSeparator = " ")
        {
            return bytes.AsMemory(NumberSeparator);
        }

        public static string AsMemory(this uint bytes, string NumberSeparator = " ")
        {
            return bytes.AsMemory(NumberSeparator);
        }

        public static string AsMemory(this int bytes, string NumberSeparator = " ")
        {
            return bytes.AsMemory(NumberSeparator);
        }

        public static string AsMemory(this double bytes, string NumberSeparator = " ")
        {
            return ((decimal)bytes).AsMemory(NumberSeparator);
        }

        public static string AsMemory(this float bytes, string NumberSeparator = " ")
        {
            return ((decimal)bytes).AsMemory(NumberSeparator);
        }

        public static string AsTime(this long miliseconds)
        {
            StringBuilder stringBuilder = new StringBuilder();
            bool flag = miliseconds >= 3600000L;
            bool flag2 = miliseconds >= 60000L;
            bool flag3 = miliseconds >= 1000L;
            if (flag)
            {
                stringBuilder.Append(Math.Round((double)miliseconds / 60000.0));
                stringBuilder.Append(" hour(s), ");
            }
            if (flag2)
            {
                stringBuilder.Append(Math.Round((double)miliseconds / 60000.0));
                stringBuilder.Append(" minute(s), ");
            }
            if (flag3)
            {
                stringBuilder.Append(Math.Round((double)miliseconds / 1000.0));
                stringBuilder.Append(" second(s).");
            }
            else
            {
                stringBuilder.Append(miliseconds);
                stringBuilder.Append(" milisecond(s).");
            }
            return stringBuilder.ToString();
        }

        public static void Raise(this EventHandler eventHandler, object sender, EventArgs args)
        {
            if (eventHandler == null)
            {
                return;
            }
            eventHandler(sender, args);
        }
    }
}
