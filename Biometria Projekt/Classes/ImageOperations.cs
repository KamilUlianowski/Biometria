using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biometria_Projekt
{
    class ImageOperations
    {
        public static double SearchMaxValue(byte[] array)
        {
            double max = 0;
            for (int i = 0; i < array.Length; i += 4)
            {
                if (array[i] > max) max = array[i];
                if (array[i + 1] > max) max = array[i + 1];
                if (array[i + 2] > max) max = array[i + 2];
            }
            return max;
        }

        public static double SearchMinValue(byte[] array)
        {
            double min = 255;
            for (int i = 0; i < array.Length; i += 4)
            {
                if (array[i] < min) min = array[i];
                if (array[i + 1] < min) min = array[i + 1];
                if (array[i + 2] < min) min = array[i + 2];
            }
            return min;
        }

        public static double SearchMaxValue2(byte[] array)
        {
            double max = 0;
            for (int i = 0; i < array.Length; i += 4)
            {
                int v = (array[i] + array[i + 1] + array[i + 2]) / 3;
                if (v > max) max = v;
            }
            return max;
        }

        public static double SearchMinValue2(byte[] array)
        {
            double min = 255;
            for (int i = 0; i < array.Length; i += 4)
            {
                int v = (array[i] + array[i + 1] + array[i + 2]) / 3;
                if (v < min) min = v;
            }
            return min;
        }

        public static int GetIndexOfPixel(int X, int Y, int _stride )
        {
            int x = 4 * X;
            int y = _stride * Y;
            return x + y;
        }


        public static int[] GetLUTForChangeTheBrightness(double x)
        {
            int[] LUT = new int[256];
               
            for (double i = 0; i < 256; i++)
            {
                LUT[(int)i] =(int) (255 * Math.Pow(i/255,x));
            }
            return LUT;
        }

        public static int[] GetLUTForChangeTheBrightness2(int x)
        {
            int[] LUT = new int[256];
            var nmb = Math.Pow((x), 2);
            for (int i = 0; i < 256; i++)
            {
                LUT[i] = (int)(255 * Math.Log(1 + i) / Math.Log(1 + 255));
            }
            return LUT;
        }

        public static int[] GetLUTForHistogramStreching(byte[] pixels, double min, double max)
        {
            int[] LUT = new int[256];

            for (int i = 0; i <= 255; i++)
            { 
                LUT[i] = (int) ((255 / (max - min)) * (i - min));
                if (LUT[i] < 0) LUT[i] = 0;
                if (LUT[i] > 255) LUT[i] = 255;
            }
            return LUT;
        }

        public static int[] GetLUTFromCumulativeHistogram(int[] cumulativeHistogram, int width, int height)
        {
            var LUT = new int[256];

            for (int i = 0; i < 256; i++)
            {
                LUT[i] = cumulativeHistogram[i] * 255 / (width * height);
            }

            return LUT;
        }

        public static int[] GetCumulativeHistogram(int[] histogram)
        {
            var cumulativeHistogram = histogram;
            cumulativeHistogram[0] = histogram[0];
            for (int i = 1; i < 256; i++)
            {
                cumulativeHistogram[i] = cumulativeHistogram[i] + cumulativeHistogram[i - 1];
            }

            return cumulativeHistogram;
        }

    }
}

